using System.Collections.Generic;
using System.Linq;
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

    public IEnumerable<IHandlerMetadata> LoadHandlersTypesForAssemblies(params Assembly[] assemblies)
    {
        var result = new List<IHandlerMetadata>();
        
        foreach (var assembly in assemblies)
        {
            var types = _cache.GetSet($"handlers_{assembly.FullName}", () => LoadTypesForAssembly(assembly));
            result.AddRange(types);
        }

        return result;
    }

    public IEnumerable<IHandlerMetadata> LoadTypesForAssembly(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>) ||
                                    i.GetGenericTypeDefinition() == typeof(ICommandHandler<>))))
            .Select(x =>
            {
                var handlerInterface = x.GetInterfaces()
                    .FirstOrDefault(i => i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>) ||
                                                    i.GetGenericTypeDefinition() == typeof(ICommandHandler<>)));

                return new HandlerMetadata
                {
                    GenericTypeDefinition = handlerInterface.GetGenericTypeDefinition(),
                    GenericArguments = handlerInterface.GetGenericArguments(),
                    IsCommand = handlerInterface.GetGenericTypeDefinition() == typeof(ICommandHandler<>),
                    Type = x
                };
            })
            .ToArray();
    }
}