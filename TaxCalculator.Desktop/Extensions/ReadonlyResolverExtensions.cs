using System;
using Splat;
using TaxCalculator.Cqrs.Contracts.Handler;

namespace TaxCalculator.Desktop.Extensions;

public static class ReadonlyResolverExtensions
{
    public static IHandler GetCommandHandler(this IReadonlyDependencyResolver resolver, Type commandType)
    {
        var commandHandlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);
        return (IHandler)resolver.GetService(commandHandlerType);
    }
    
    public static IHandler GetQueryHandler(this IReadonlyDependencyResolver resolver, Type queryType, Type resultType)
    {
        var args = new[] {queryType, resultType};
        var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(args);

        return (IHandler)resolver.GetService(queryHandlerType);
    }
}