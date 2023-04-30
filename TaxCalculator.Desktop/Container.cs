using System;
using System.Collections.Generic;
using System.Linq;
using Splat;

namespace TaxCalculator.Desktop;

public class ServiceInfo
{
    public Type ServiceType { get; set; }

    public Type AbstractionType { get; set; }

    public List<Type> DependencyTypes { get; set; }
}

public interface IContainer
{
    void Register<TService>();
    void Register(Type serviceType);
    void Register<TAbstraction, TService>();
    void Register(Type abstractionType, Type serviceType);
    void Compile();
}

public class Container : IContainer
{
    private readonly Dictionary<Type, ServiceInfo> _dependencyMap;
    private readonly Dictionary<Type, Type> _abstractionMap;
    private readonly IMutableDependencyResolver _services;
    
    public Container()
    {
        _dependencyMap = new();
        _abstractionMap = new();
        _services = Locator.CurrentMutable;
    }

    public void Register<TService>()
    {
        AddToDependencyMap(null, typeof(TService));
    }

    public void Register(Type serviceType)
    {
        AddToDependencyMap(null, serviceType);
    }
    
    public void Register<TAbstraction, TService>()
    {
        AddToDependencyMap(typeof(TAbstraction), typeof(TService));
    }
    
    public void Register(Type abstractionType, Type serviceType)
    {
        AddToDependencyMap(abstractionType, serviceType);
    }

    public void Compile()
    {
        foreach (var dependencyMapItem in _dependencyMap)
        {
            _services.Register(() => BuildService(dependencyMapItem.Value), dependencyMapItem.Value.AbstractionType ?? dependencyMapItem.Key);
        }
    }
    
    private void AddToDependencyMap(Type abstractionType, Type type)
    {
        var parameterTypes = new List<Type>();
            
        var typeConstructor = type.GetConstructors().FirstOrDefault();
        if (typeConstructor != null)
        {
            var constructorArgumentTypes = typeConstructor.GetParameters()
                .Select(x => x.ParameterType).ToArray();
            
            parameterTypes.AddRange(constructorArgumentTypes);
        }

        _dependencyMap.Add(type, new ServiceInfo
        {
            AbstractionType = abstractionType,
            ServiceType = type,
            DependencyTypes = parameterTypes
        });

        if (abstractionType != null)
        {
            _abstractionMap.Add(abstractionType, type);
        }
    }
    
    private object BuildService(ServiceInfo serviceInfo)
    {
        var serviceDependencies = new List<object>();

        foreach (var serviceDependencyType in serviceInfo.DependencyTypes)
        {
            if (!_dependencyMap.TryGetValue(serviceDependencyType, out ServiceInfo dependencyInfo))
            {
                if (!_abstractionMap.TryGetValue(serviceDependencyType, out Type type))
                {
                    throw new Exception($"Type {serviceDependencyType} has not been registered in the container");
                }

                dependencyInfo = _dependencyMap[type];
            }
            
            var serviceDependency = BuildService(dependencyInfo);
            serviceDependencies.Add(serviceDependency);
        }
        
        return Activator.CreateInstance(serviceInfo.ServiceType, serviceDependencies.ToArray());
    }
}