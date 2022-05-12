using System.Reflection;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Pages.Dashboard.Queries;

namespace TaxCalculator.WebFrontend;

public static class ServiceProviderExtension
{
    public static IHandler GetCommandHandler(this IServiceProvider provider, Type commandType)
    {
        var loader = provider.GetService<IHandlerLoader>();
        return loader.LoadHandlersTypesForAssemblies(Assembly.GetAssembly(typeof(GetTaxStatisticsQuery)))
            .Where(x => x.IsCommand && x.GenericArguments.Contains(commandType))
            .Select(x => (IHandler) ActivatorUtilities.GetServiceOrCreateInstance(provider, x.Type))
            .FirstOrDefault();
    }
    
    public static IHandler GetQueryHandler(this IServiceProvider provider, Type queryType, Type resultType)
    {
        var args = new[] {queryType, resultType};
        var loader = provider.GetService<IHandlerLoader>();
        return loader.LoadHandlersTypesForAssemblies(Assembly.GetAssembly(typeof(GetTaxStatisticsQuery)))
            .Where(x => x.IsCommand == false && x.GenericArguments.All(arg => args.Contains(arg)))
            .Select(x => (IHandler) ActivatorUtilities.GetServiceOrCreateInstance(provider, x.Type))
            .FirstOrDefault();
    }
}