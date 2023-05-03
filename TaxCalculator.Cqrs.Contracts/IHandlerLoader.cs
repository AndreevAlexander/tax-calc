﻿using System.Reflection;

namespace TaxCalculator.Cqrs.Contracts;

public interface IHandlerLoader
{
    IEnumerable<IHandlerMetadata> LoadHandlersTypesForAssemblies(params Assembly[] assemblies);

    IEnumerable<IHandlerMetadata> LoadTypesForAssembly(Assembly assembly);
}