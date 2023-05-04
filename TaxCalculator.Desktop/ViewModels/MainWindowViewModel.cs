using System;
using System.Reactive;
using ReactiveUI;
using Splat;
using TaxCalculator.Desktop.Controls;
using TaxCalculator.Desktop.ViewModels.BaseTypes;

namespace TaxCalculator.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; }
    
    public ReactiveCommand<NavigationMenuItem, Unit> MenuItemClickCommand { get; }

    public MainWindowViewModel()
    {
        Router = new RoutingState();
        MenuItemClickCommand = ReactiveCommand.Create<NavigationMenuItem>(ItemClickCommandExecute);
    }

    private void ItemClickCommandExecute(NavigationMenuItem parameter)
    {
        var typeName = parameter.Tag.ToString();
        var type = Type.GetType(typeName);

        var viewModel = Locator.Current.GetService(type) as RoutedViewModel;

        viewModel.HostScreen = this;
        
        Router.Navigate.Execute(viewModel);
    }
}