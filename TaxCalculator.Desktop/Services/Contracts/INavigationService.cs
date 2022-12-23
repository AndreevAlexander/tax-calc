using System.Windows.Controls;

namespace TaxCalculator.Desktop.Services.Contracts;

public interface INavigationService
{
    Frame RootFrame { get; }
    
    void NavigateTo<TView>() where TView : class;

    object GetCurrentView();
}