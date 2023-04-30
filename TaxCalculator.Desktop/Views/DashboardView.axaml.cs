using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using TaxCalculator.Desktop.ViewModels;

namespace TaxCalculator.Desktop.Views;

public partial class DashboardView : ReactiveUserControl<DashboardViewModel>
{
    public DashboardView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}