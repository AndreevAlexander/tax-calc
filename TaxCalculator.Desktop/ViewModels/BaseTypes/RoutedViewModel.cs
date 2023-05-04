using System;
using ReactiveUI;

namespace TaxCalculator.Desktop.ViewModels.BaseTypes;

public abstract class RoutedViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment { get; }
    
    public IScreen HostScreen { get; set; }

    public RoutedViewModel()
    {
        UrlPathSegment = Guid.NewGuid().ToString().Substring(0, 5);
    }

    public abstract void OnBeforeNavigated(object parameter);
}