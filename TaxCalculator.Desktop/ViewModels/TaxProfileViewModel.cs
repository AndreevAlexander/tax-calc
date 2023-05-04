using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ReactiveUI;
using TaxCalculator.Application.TaxProfiles.Commands;
using TaxCalculator.Application.TaxProfiles.Queries;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Desktop.ViewModels.BaseTypes;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileViewModel : NestedRoutedViewModel
{
    private readonly IQueryBus _queryBus;
    
    private readonly ICommandBus _commandBus;

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

    public TaxProfileViewModel(IQueryBus queryBus,
                               ICommandBus commandBus,
                               IMapper mapper)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
        _mapper = mapper;

        EditCommand = ReactiveCommand.Create(EditExecute, this.WhenAnyValue(x => x.SelectedTaxProfile).Select(x => x != null));
        RemoveCommand = ReactiveCommand.CreateFromTask(RemoveExecuteAsync, this.WhenAnyValue(x => x.SelectedTaxProfile).Select(x => x != null));

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
        NavigateTo<TaxProfileEditViewModel>(vm => vm.TaxProfile = SelectedTaxProfile);
    }

    private async Task RemoveExecuteAsync()
    {
        var result = await _commandBus.DispatchAsync(new RemoveTaxProfileCommand
            { TaxProfileId = SelectedTaxProfile.Id });
        if (result.Status == CommandStatus.Success)
        {
            var taxProfiles = await TaxProfiles.ToTask();
            taxProfiles.Remove(SelectedTaxProfile);
        }
    }
}