using System.Reflection;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;

namespace TaxCalculator.WebApi.Extensions;

public static class ServiceProviderExtension
{
    public static IEnumerable<IHandler> GetCqrsHandlers(this IServiceProvider provider)
    {
        var cache = provider.GetService<ICache>();

        var handlers = cache.GetSet("handler_objects", () =>
        {
            var loader = provider.GetService<IHandlerLoader>();
            var handlers = loader.LoadHandlersTypesForAssemblies(Assembly.GetAssembly(typeof(CreateTaxProfileCommand)))
                .Select(x => (IHandler)ActivatorUtilities.GetServiceOrCreateInstance(provider, x)).ToList();
            
            return handlers;
        });
    
        return handlers;
    }
}