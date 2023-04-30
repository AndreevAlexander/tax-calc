using System;
using ReactiveUI;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileViewModel : ViewModelBase, IRoutableViewModel
{
    public string UrlPathSegment { get; }
    
    public IScreen HostScreen { get; }

    public TaxProfileViewModel(IScreen screen)
    {
        HostScreen = screen;
        UrlPathSegment = Guid.NewGuid().ToString().Substring(0, 5);
    }
}