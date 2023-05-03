using System;
using System.Linq;
using System.Reflection;
using Splat;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;

namespace TaxCalculator.Desktop.Extensions;

public static class ReadonlyResolverExtensions
{
    public static IHandler GetCommandHandler(this IReadonlyDependencyResolver resolver, Type commandType)
    {
        var loader = resolver.GetService<IHandlerLoader>();
        return loader.LoadHandlersTypesForAssemblies(Assembly.GetAssembly(typeof(CreateTaxProfileCommand)))
            .Where(x => x.IsCommand && x.GenericArguments.Contains(commandType))
            .Select(x => (IHandler) resolver.GetService(x.Type))
            .FirstOrDefault();
    }
    
    public static IHandler GetQueryHandler(this IReadonlyDependencyResolver resolver, Type queryType, Type resultType)
    {
        var args = new[] {queryType, resultType};
        var loader = resolver.GetService<IHandlerLoader>();
        return loader.LoadHandlersTypesForAssemblies(Assembly.GetAssembly(typeof(CreateTaxProfileCommand)))
            .Where(x => x.IsCommand == false && x.GenericArguments.All(arg => args.Contains(arg)))
            .Select(x => (IHandler) resolver.GetService(x.Type))
            .FirstOrDefault();
    }
}