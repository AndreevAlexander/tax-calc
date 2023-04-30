using System;
using ReactiveUI;
using TaxCalculator.Desktop.ViewModels;
using TaxCalculator.Desktop.Views;

namespace TaxCalculator.Desktop;

public class AppViewLocator : IViewLocator
{
    public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
    {
        DashboardViewModel context => new DashboardView { DataContext = context },
        TaxProfileViewModel context => new TaxProfileView { DataContext = context },
        _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
    };
}