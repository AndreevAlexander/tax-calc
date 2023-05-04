﻿using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
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
using TaxCalculator.Desktop.ViewModels.BaseTypes;
using TaxCalculator.Domain.Services.Identifier;
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
        Container.Register(_ =>
        {
            var options = new DbContextOptionsBuilder<TaxContext>();
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            return new TaxContext(options.Options);
        });
        
        Container.Register<IEntityManager, EntityManager>(resolver =>
        {
            var cache = resolver.GetService<ICache>();
            var dbContext = resolver.GetService<TaxContext>();
            return new EntityManager(dbContext, cache);
        });
    }

    private void RegisterServices()
    {
        Container.Register<IQueryBus, QueryBus>(resolver => new QueryBus(resolver.GetQueryHandler));
        Container.Register<ICommandBus, CommandBus>(resolver => new CommandBus(resolver.GetCommandHandler));
        Container.Register<IHandlerLoader, HandlerLoader>();
        Container.Register<IMapper, MapperDecorator>(_ => new MapperDecorator(new UiMappingBuilder()));
        Container.Register<ICache, StaticCache>();
        Container.Register<IIdentifierService, IdentifierService>();
    }

    private void RegisterViewModels()
    {
        Container.Register<IScreen, MainWindowViewModel>();

        var baseViewModelTypes = new[]
            { typeof(ViewModelBase), typeof(RoutedViewModel), typeof(NestedRoutedViewModel) };
        var viewModelTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(x => x.BaseType != null && baseViewModelTypes.Contains(x.BaseType) && x != typeof(MainWindowViewModel))
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