using System.Reflection;

namespace TaxCalculator.Cqrs.Contracts;

public interface IHandlerLoader
{
    IEnumerable<Type> LoadHandlersTypesForAssemblies(params Assembly[] assemblies);
}