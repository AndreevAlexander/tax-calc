using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ReactiveUI;
using TaxCalculator.Application.TaxProfiles.Queries;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileViewModel : ViewModelBase, IRoutableViewModel
{
    private readonly IQueryBus _queryBus;
    
    private readonly IMapper _mapper;

    public IObservable<ObservableCollection<TaxProfileModel>> TaxProfiles { get; }

    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public ReactiveCommand<Unit, Unit> RemoveCommand { get; }

    private TaxProfileModel _selectedTaxProfile;
    public TaxProfileModel SelectedTaxProfile
    { 
        get => _selectedTaxProfile;
        set => this.RaiseAndSetIfChanged(ref _selectedTaxProfile, value);
    }

    public string UrlPathSegment { get; }
    
    public IScreen HostScreen { get; }

    public TaxProfileViewModel(IScreen screen,
                               IQueryBus queryBus,
                               IMapper mapper)
    {
        _queryBus = queryBus;
        _mapper = mapper;
        HostScreen = screen;
        UrlPathSegment = Guid.NewGuid().ToString().Substring(0, 5);
        
        EditCommand = ReactiveCommand.Create(EditExecute, this.WhenAnyValue(x => x.SelectedTaxProfile).Select(x => x != null));
        RemoveCommand = ReactiveCommand.Create(RemoveExecute, this.WhenAnyValue(x => x.SelectedTaxProfile).Select(x => x != null));

        TaxProfiles = LoadData().ToObservable();
    }

    private async Task<ObservableCollection<TaxProfileModel>> LoadData()
    {
        var taxProfiles =
            await _queryBus.ExecuteAsync<GetTaxProfilesQuery, List<TaxProfile>>(new GetTaxProfilesQuery());

        return _mapper.Map<ObservableCollection<TaxProfileModel>>(taxProfiles);
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