using System;
using ReactiveUI;
using Splat;

namespace TaxCalculator.Desktop.ViewModels.BaseTypes;

public abstract class NestedRoutedViewModel : RoutedViewModel, IScreen
{
    public RoutingState Router { get; }
    
    public NestedRoutedViewModel()
    {
        Router = new RoutingState();
    }

    protected void NavigateTo<TViewModel>(Action<TViewModel> parametersSetter = null) where TViewModel : RoutedViewModel
    {
        var viewModel = Locator.Current.GetService<TViewModel>();
        if (viewModel == null)
        {
            throw new Exception("View model not registered in container");
        }

        viewModel.HostScreen = this;
        
        parametersSetter?.Invoke(viewModel);
        
        Router.Navigate.Execute(viewModel);
    }
}