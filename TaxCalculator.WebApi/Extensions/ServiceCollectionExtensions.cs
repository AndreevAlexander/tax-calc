using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.Incomes;
using TaxCalculator.Application.Services;
using TaxCalculator.Application.TaxProfiles;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Cqrs.Implementation;
using TaxCalculator.Cqrs.Implementation.Bus;
using TaxCalculator.Data;
using TaxCalculator.Domain.Services;
using TaxCalculator.Domain.Services.Identifier;
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
        services.AddSingleton<IIdentifierService, IdentifierService>();
        services.AddScoped<ICurrencyConverterService, CurrencyConverterService>();
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
            var engine = new ValidationEngine(ruleType => 
                (IValidationRule)ActivatorUtilities.GetServiceOrCreateInstance(provider, ruleType));
            
            engine.RegisterValidationProfile<TaxProfileValidationProfile>();
            engine.RegisterValidationProfile<IncomeValidationProfile>();
          
            return engine;
        });
    }
}