using System.Reflection;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;

namespace TaxCalculator.Cqrs.Implementation;

public class HandlerLoader : IHandlerLoader
{
    private readonly ICache _cache;

    public HandlerLoader(ICache cache)
    {
        _cache = cache;
    }

    public IEnumerable<Type> LoadHandlersTypesForAssemblies(params Assembly[] assemblies)
    {
        var result = new List<Type>();
        
        foreach (var assembly in assemblies)
        {
            var types = _cache.GetSet($"handlers_{assembly.FullName}", () => LoadTypesForAssembly(assembly));
            result.AddRange(types);
        }

        return result;
    }

    private IEnumerable<Type> LoadTypesForAssembly(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>) ||
                                    i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)))).ToArray();
    }
}