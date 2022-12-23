using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace TaxCalculator.Desktop.Helpers.ViewFactory;

public static class ServiceCollectionExtensions
{
    public static void RegisterView<TView, TViewModel>(this IServiceCollection services) 
        where TView : class
        where TViewModel : ObservableObject
    {
        services.AddTransient<TView>();
        services.AddSingleton<Func<TView>>(provider => () => provider.GetService<TView>());
        services.AddSingleton<IViewFactory<TView>, ViewFactory<TView>>();
        services.AddTransient<TViewModel>();
    }
    
    public static void RegisterView<TView>(this IServiceCollection services) 
        where TView : class
    {
        services.AddTransient<TView>();
        services.AddSingleton<Func<TView>>(provider => () => provider.GetService<TView>());
        services.AddSingleton<IViewFactory<TView>, ViewFactory<TView>>();
    }
}