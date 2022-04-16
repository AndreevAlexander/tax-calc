using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.TaxProfiles;
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
        services.AddScoped<IQueryBus, QueryBus>(provider => new QueryBus(provider.GetQueryHandler));
    
        services.AddScoped<ICommandBus, CommandBus>(provider => new CommandBus(provider.GetCommandHandler));
    }

    public static void AddValidation(this IServiceCollection services)
    {
        services.AddSingleton<IValidationEngine, ValidationEngine>(provider =>
        {
            var engine = new ValidationEngine(provider);
            engine.RegisterValidationProfile<TaxProfileValidationProfile>();
            return engine;
        });
    }
}