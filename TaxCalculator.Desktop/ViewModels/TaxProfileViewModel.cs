using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileViewModel : ViewModelBase, IRoutableViewModel
{
    public ObservableCollection<TaxProfile> TaxProfiles { get; set; }
    
    public string UrlPathSegment { get; }
    
    public IScreen HostScreen { get; }

    public TaxProfileViewModel(IScreen screen)
    {
        HostScreen = screen;
        UrlPathSegment = Guid.NewGuid().ToString().Substring(0, 5);

        TaxProfiles = new ObservableCollection<TaxProfile>();
        TaxProfiles.Add(new TaxProfile
        {
            Id = Guid.NewGuid(),
            Description = "test",
            Name = "test",
            CreatedDate = DateTime.Now,
            IsDeleted = false,
        });
    }
}