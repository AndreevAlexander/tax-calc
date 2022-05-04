using System.Collections.Generic;
using System.Reflection;

namespace TaxCalculator.Cqrs.Contracts;

public interface IHandlerLoader
{
    IEnumerable<IHandlerMetadata> LoadHandlersTypesForAssemblies(params Assembly[] assemblies);
}