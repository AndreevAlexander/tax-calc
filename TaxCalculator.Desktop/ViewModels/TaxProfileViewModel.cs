using System;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileViewModel : ViewModelBase, IRoutableViewModel
{
    public ObservableCollection<TaxProfileModel> TaxProfiles { get; }

    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    public TaxProfileModel SelectedTaxProfile { get; set; }

    public string UrlPathSegment { get; }
    
    public IScreen HostScreen { get; }

    public TaxProfileViewModel(IScreen screen)
    {
        HostScreen = screen;
        UrlPathSegment = Guid.NewGuid().ToString().Substring(0, 5);

        EditCommand = ReactiveCommand.Create(EditExecute);
        RemoveCommand = ReactiveCommand.Create(RemoveExecute);

        TaxProfiles = new ObservableCollection<TaxProfileModel>
        {
            new TaxProfileModel
            {
                Id = Guid.NewGuid(),
                Description = "test",
                Name = "test",
                Currency = new CurrencyModel
                {
                    Name = "UAH"
                }
            }
        };
    }

    private void EditExecute()
    {
        Console.WriteLine(SelectedTaxProfile.Id);
    }

    private void RemoveExecute()
    {
        Console.WriteLine(SelectedTaxProfile.Id);
    }
}