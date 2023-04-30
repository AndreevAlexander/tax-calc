using System;
using System.Reactive;
using ReactiveUI;
using TaxCalculator.Desktop.Controls;

namespace TaxCalculator.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";
    
    public ReactiveCommand<NavigationMenuItem, Unit> MenuItemClickCommand { get; }

    public MainWindowViewModel()
    {
        //var canExecuteMenuItemClickCommand = this.WhenAnyValue(model => model.Greeting, v => !string.IsNullOrEmpty(v));
        MenuItemClickCommand = ReactiveCommand.Create<NavigationMenuItem>(ItemClickCommandExecute);
    }

    private void ItemClickCommandExecute(NavigationMenuItem parameter)
    {
        var title = parameter.Title;
        Console.WriteLine(title);
    }
}