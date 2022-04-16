using System.Reflection;
using TaxCalculator.Cqrs.Implementation;

namespace TaxCalculator.Cqrs.Contracts;

public interface IHandlerLoader
{
    IEnumerable<HandlerMetadata> LoadHandlersTypesForAssemblies(params Assembly[] assemblies);
}