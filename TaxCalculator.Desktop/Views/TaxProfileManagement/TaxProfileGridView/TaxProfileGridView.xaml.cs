using System.Windows.Controls;

namespace TaxCalculator.Desktop.Views.TaxProfileManagement.TaxProfileGridView;

public partial class TaxProfileGridView : Page
{
    public TaxProfileGridView(TaxProfileGridViewModel viewModel)
    {
        InitializeComponent();

        DataContext = viewModel;
    }
}