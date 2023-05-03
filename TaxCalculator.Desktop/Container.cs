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
    void Register<TService>(Func<IReadonlyDependencyResolver, TService> factory);
    void  Register(Type serviceType);
    void Register<TAbstraction, TService>();
    void Register<TAbstraction, TService>(Func<IReadonlyDependencyResolver, TAbstraction> factory);
    void Register(Type abstractionType, Type serviceType);
    void Compile();
}

public class Container : IContainer
{
    private readonly Dictionary<Type, ServiceInfo> _dependencyMap;
    private readonly Dictionary<Type, Type> _abstractionMap;
    private readonly IMutableDependencyResolver _services;
    private readonly IReadonlyDependencyResolver _resolver;
    
    public Container()
    {
        _dependencyMap = new();
        _abstractionMap = new();
        _services = Locator.CurrentMutable;
        _resolver = Locator.Current;
    }

    public void Register<TService>()
    {
        AddToDependencyMap(null, typeof(TService));
    }

    public void Register<TService>(Func<IReadonlyDependencyResolver, TService> factory)
    {
        _services.Register(() => factory(_resolver));
    }

    public void Register(Type serviceType)
    {
        AddToDependencyMap(null, serviceType);
    }
    
    public void Register<TAbstraction, TService>()
    {
        AddToDependencyMap(typeof(TAbstraction), typeof(TService));
    }

    public void Register<TAbstraction, TService>(Func<IReadonlyDependencyResolver, TAbstraction> factory)
    {
        _services.Register(() => factory(_resolver));
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
            var addedDependencyViaResolver = false;
            if (!_dependencyMap.TryGetValue(serviceDependencyType, out var dependencyInfo))
            {
                if (_abstractionMap.TryGetValue(serviceDependencyType, out var type))
                {
                    dependencyInfo = _dependencyMap[type];
                }
                else
                {
                    var dependency = _resolver.GetService(serviceDependencyType);
                    if (dependency == null)
                    {
                        throw new Exception($"Type {serviceDependencyType} has not been registered in the container");
                    }
                    
                    serviceDependencies.Add(dependency);
                    addedDependencyViaResolver = true;
                }
            }

            if (!addedDependencyViaResolver)
            {
                var serviceDependency = BuildService(dependencyInfo);
                serviceDependencies.Add(serviceDependency);
            }
        }
        
        return Activator.CreateInstance(serviceInfo.ServiceType, serviceDependencies.ToArray());
    }
}