using System;
using Avalonia.Controls;
using ReactiveUI;
using TaxCalculator.Desktop.ViewModels;
using TaxCalculator.Desktop.Views;

namespace TaxCalculator.Desktop;

public class AppViewLocator : IViewLocator 
{
    /*public IViewFor ResolveView<T>(T viewModel, string contract = null) => viewModel switch
    {
        DashboardViewModel context => new DashboardView { DataContext = context },
        TaxProfileViewModel context => new TaxProfileView { DataContext = context },
        _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
    };*/

    private const string ViewsNamespace = "TaxCalculator.Desktop.Views";
    
    public IViewFor ResolveView<T>(T viewModel, string contract = null)
    {
        var type = viewModel.GetType();
        var viewModelName = type.Name;
        var viewName = viewModelName.Replace("Model", "");

        var viewFullName = $"{ViewsNamespace}.{viewName}";
        var viewType = Type.GetType(viewFullName);
        if (viewType == null)
        {
            throw new ArgumentOutOfRangeException(nameof(viewModel));
        }

        var view = Activator.CreateInstance(viewType) as UserControl;
        view.DataContext = viewModel;

        return view as IViewFor;
    }
}