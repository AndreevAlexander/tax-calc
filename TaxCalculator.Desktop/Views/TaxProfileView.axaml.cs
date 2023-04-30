using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using TaxCalculator.Desktop.ViewModels;

namespace TaxCalculator.Desktop.Views;

public partial class TaxProfileView : ReactiveUserControl<TaxProfileViewModel>
{
    public TaxProfileView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}