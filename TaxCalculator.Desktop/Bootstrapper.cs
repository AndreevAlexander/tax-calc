using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using ReactiveUI;
using Splat;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Implementation;
using TaxCalculator.Cqrs.Implementation.Bus;
using TaxCalculator.Data;
using TaxCalculator.Desktop.Extensions;
using TaxCalculator.Desktop.ViewModels;
using TaxCalculator.Infrastructure;
using TaxCalculator.Infrastructure.Mapper;
using TaxCalculator.Persistence;

namespace TaxCalculator.Desktop;

public class Bootstrapper
{
    public IContainer Container { get; }
    
    private IConfiguration Configuration { get; }
    
    public Bootstrapper(IContainer container)
    {
        Container = container;
        Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
    }
    
    public void BootstrapServices()
    {
        RegisterDatabase();
        RegisterServices();
        RegisterViewModels();
        RegisterCqrsHandlers();
    }

    private void RegisterDatabase()
    {
        Container.Register<IEntityManager, EntityManager>(resolver =>
        {
            var cache = resolver.GetService<ICache>();
            var options = new DbContextOptionsBuilder<TaxContext>();
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            var dbContext = new TaxContext(options.Options);
            return new EntityManager(dbContext, cache);
        });
    }

    private void RegisterServices()
    {
        Container.Register<IQueryBus, QueryBus>(resolver => new QueryBus(resolver.GetQueryHandler));
        Container.Register<ICommandBus, CommandBus>(resolver => new CommandBus(resolver.GetCommandHandler));
        Container.Register<IHandlerLoader, HandlerLoader>();
        Container.Register<IMapper, MapperDecorator>(resolver => new MapperDecorator(new UiMappingBuilder()));
        Container.Register<ICache, StaticCache>();
    }

    private void RegisterViewModels()
    {
        Container.Register<IScreen, MainWindowViewModel>();
        
        var viewModelTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.BaseType != null && x.BaseType == typeof(ViewModelBase) && x != typeof(MainWindowViewModel))
            .ToArray();

        foreach (var viewModelType in viewModelTypes)
        {
            Container.Register(viewModelType);
        }
    }

    private void RegisterCqrsHandlers()
    {
        var handlerLoader = new HandlerLoader(null);
        var handlerMetadataItems = handlerLoader
            .LoadTypesForAssembly(Assembly.GetAssembly(typeof(CreateTaxProfileCommand)));

        foreach (var handlerMetadataItem in handlerMetadataItems)
        {
            var handlerAbstraction =
                handlerMetadataItem.GenericTypeDefinition.MakeGenericType(
                    handlerMetadataItem.GenericArguments.ToArray());
            Container.Register(handlerAbstraction, handlerMetadataItem.Type);
        }
    }
}