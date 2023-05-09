using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using TaxCalculator.Desktop.ViewModels;

namespace TaxCalculator.Desktop.Views;

public partial class AdditionalSpendView : ReactiveUserControl<AdditionalSpendViewModel>
{
    public AdditionalSpendView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}