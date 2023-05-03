using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Splat;
using TaxCalculator.Desktop.ViewModels;
using TaxCalculator.Desktop.Views;

namespace TaxCalculator.Desktop;

public partial class App : Avalonia.Application
{
    private Bootstrapper _bootstrapper;
    
    public override void Initialize()
    {
        _bootstrapper = new Bootstrapper(new Container());
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        _bootstrapper.BootstrapServices();
        _bootstrapper.Container.Compile();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Locator.Current.GetService<IScreen>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}