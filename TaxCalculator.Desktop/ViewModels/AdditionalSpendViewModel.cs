using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ReactiveUI;
using TaxCalculator.Application.AdditionalSpends.Commands;
using TaxCalculator.Application.AdditionalSpends.Queries;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Desktop.ViewModels.BaseTypes;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Desktop.ViewModels;

public class AdditionalSpendViewModel : RoutedViewModel
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    private readonly IMapper _mapper;

    private TaxProfileModel _taxProfile;
    private AdditionalSpendModel _additionalSpendModel;

    public AdditionalSpendModel SelectedItem
    {
        get => _additionalSpendModel;
        set => this.RaiseAndSetIfChanged(ref _additionalSpendModel, value);
    }
    public IObservable<ObservableCollection<AdditionalSpendModel>> AdditionalSpends { get; set; }
    
    public ReactiveCommand<Unit, Unit> AddAdditionalSpendCommand { get; }
    
    public ReactiveCommand<Unit, Unit> RemoveAdditionalSpendCommand { get; }
    
    public ReactiveCommand<Unit, Unit> SaveAdditionalSpendsCommand { get; }

    public AdditionalSpendViewModel(IQueryBus queryBus,
                                    ICommandBus commandBus,
                                    IMapper mapper)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
        _mapper = mapper;

        AddAdditionalSpendCommand = ReactiveCommand.CreateFromTask(AddAdditionalSpendCommandExecuteAsync);
        RemoveAdditionalSpendCommand = ReactiveCommand.CreateFromTask(RemoveAdditionalSpendCommandExecuteAsync);
        SaveAdditionalSpendsCommand = ReactiveCommand.CreateFromTask(SaveAdditionalSpendsCommandExecuteAsync);
    }
    
    public override void OnBeforeNavigated(object parameter)
    {
        _taxProfile = parameter as TaxProfileModel;
        AdditionalSpends = LoadData().ToObservable();
    }

    private async Task<ObservableCollection<AdditionalSpendModel>> LoadData()
    {
        var data =
            await _queryBus.ExecuteAsync<GetAdditionalSpendsQuery, List<AdditionalSpend>>(
                new GetAdditionalSpendsQuery { ProfileId = _taxProfile.Id });

        return _mapper.Map<ObservableCollection<AdditionalSpendModel>>(data);
    }

    private async Task RemoveAdditionalSpendCommandExecuteAsync()
    {
        var result = await _commandBus.DispatchAsync(new RemoveAdditionalSpendCommand
            { AdditionalSpendId = SelectedItem.Id });
        if (result.Status == CommandStatus.Success)
        {
            var additionalSpends = await AdditionalSpends.ToTask();
            additionalSpends.Remove(SelectedItem);
        }
    }

    private async Task AddAdditionalSpendCommandExecuteAsync()
    {
        var additionalSpends = await AdditionalSpends.ToTask();
        additionalSpends.Add(new AdditionalSpendModel { TaxProfileId = _taxProfile.Id });
    }

    private async Task SaveAdditionalSpendsCommandExecuteAsync()
    {
        var additionalSpends = await AdditionalSpends.ToTask();
        foreach (var additionalSpend in additionalSpends)
        {
            if (additionalSpend.Id == Guid.Empty)
            {
                await _commandBus.DispatchAsync(_mapper.Map<AddAdditionalSpendCommand>(additionalSpend));
            }
            else
            {
                await _commandBus.DispatchAsync(_mapper.Map<UpdateAdditionalSpendCommand>(additionalSpend));
            }
        }
    }
}