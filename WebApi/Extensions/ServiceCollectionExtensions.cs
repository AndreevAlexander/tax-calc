using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Implementation;
using TaxCalculator.Cqrs.Implementation.Bus;
using TaxCalculator.Data;
using TaxCalculator.Infrastructure;
using TaxCalculator.Infrastructure.Mapper;
using TaxCalculator.Persistence;
using TaxCalculator.Validation;
using TaxCalculator.Validation.Contracts;

namespace TaxCalculator.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<MappingBuilder>();
        services.AddSingleton<IMapper, MapperDecorator>();
        services.AddSingleton<ICache, CacheDecorator>();
        services.AddSingleton<IHandlerLoader, HandlerLoader>();
    }

    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaxContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IEntityManager, EntityManager>();
    }

    public static void AddCqrs(this IServiceCollection services)
    {
        services.AddScoped<IQueryBus, QueryBus>(provider =>
        {
            var handlers = provider.GetCqrsHandlers();
            return new QueryBus(handlers.ToList());
        });
    
        services.AddScoped<ICommandBus, CommandBus>(provider =>
        {
            var handlers = provider.GetCqrsHandlers();
            return new CommandBus(handlers.ToList());
        });
    }

    public static void AddValidation(this IServiceCollection services)
    {
        var profile = new ValidationProfile();
        profile.ForModel<CreateTaxProfileCommand>(b =>
        {
            b.Property(nameof(CreateTaxProfileCommand.Name))
                .MinLength(5);

            b.Property(nameof(CreateTaxProfileCommand.Description))
                .Required();
        });
        
        services.AddSingleton<IValidationEngine, ValidationEngine>(provider =>
        {
            var engine = new ValidationEngine(provider);
            engine.RegisterValidationProfile(profile);
            return engine;
        });
    }
}