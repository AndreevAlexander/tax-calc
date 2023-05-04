using System;
using ReactiveUI;
using TaxCalculator.Desktop.ViewModels.BaseTypes;

namespace TaxCalculator.Desktop.ViewModels;

public class DashboardViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment { get; }
    
    public IScreen HostScreen { get; }

    public DashboardViewModel(IScreen screen)
    {
        HostScreen = screen;
        UrlPathSegment = Guid.NewGuid().ToString().Substring(0, 5);
    }
}