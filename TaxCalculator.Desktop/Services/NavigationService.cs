using System;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Desktop.Helpers.ViewFactory;
using TaxCalculator.Desktop.Services.Contracts;

namespace TaxCalculator.Desktop.Services;

public class NavigationService : INavigationService
{
    private readonly Frame _rootFrame;
    
    private readonly IServiceProvider _serviceProvider;

    public Frame RootFrame => _rootFrame;
    
    public NavigationService(Frame rootFrame, IServiceProvider serviceProvider)
    {
        _rootFrame = rootFrame;
        _serviceProvider = serviceProvider;
    }
    
    public void NavigateTo<TView>() where TView : class
    {
        var factory = _serviceProvider.GetService<IViewFactory<TView>>();

        _rootFrame.Navigate(factory.Create());
    }

    public object GetCurrentView()
    {
        return _rootFrame.Content;
    }
}