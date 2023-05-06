using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ReactiveUI;
using TaxCalculator.Application.Taxes.Commands;
using TaxCalculator.Application.Taxes.Queries;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Desktop.ViewModels.BaseTypes;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Services.Identifier;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileEditViewModel : RoutedViewModel
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;
    private readonly IMapper _mapper;

    private KeyValuePair<Guid, string> _selectedCurrency;
    private TaxModel _selectedTaxConfiguration;
    
    public TaxProfileModel TaxProfile { get; set; }

    public KeyValuePair<Guid, string> SelectedCurrency
    {
        get => _selectedCurrency;
        set => this.RaiseAndSetIfChanged(ref _selectedCurrency, value);
    }

    public Dictionary<Guid, string> Currencies { get; }

    public IObservable<ObservableCollection<TaxModel>> TaxConfigurations { get; set; }

    public ReactiveCommand<Unit, Unit> SaveTaxConfigurationsCommand { get; set; }
    
    public ReactiveCommand<Unit, Unit> AddTaxConfigurationCommand { get; set; }
    
    public ReactiveCommand<Unit, Unit> RemoveTaxConfigurationCommand { get; set; }

    public TaxModel SelectedTaxConfiguration
    {
        get => _selectedTaxConfiguration;
        set => this.RaiseAndSetIfChanged(ref _selectedTaxConfiguration, value);
    }

    public TaxProfileEditViewModel(IIdentifierService identifierService,
                                   IQueryBus queryBus,
                                   ICommandBus commandBus,
                                   IMapper mapper)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
        _mapper = mapper;
        Currencies = identifierService.Currencies.ToDictionary();

        SaveTaxConfigurationsCommand = ReactiveCommand.CreateFromTask(SaveTaxConfigurationsExecuteAsync);
        AddTaxConfigurationCommand = ReactiveCommand.CreateFromTask(AddTaxConfigurationExecuteAsync);
        RemoveTaxConfigurationCommand = ReactiveCommand.CreateFromTask(RemoveTaxConfigurationExecuteAsync,
            this.WhenAnyValue(vm => vm.SelectedTaxConfiguration).Select(x => x != null));
    }

    public override void OnBeforeNavigated(object parameter)
    {
        if (parameter is TaxProfileModel model)
        {
            TaxProfile = model;
            SelectedCurrency = new KeyValuePair<Guid, string>(model.Currency.Id, model.Currency.Name);
            TaxConfigurations = LoadTaxConfigurationsAsync().ToObservable();
        }
    }

    private async Task<ObservableCollection<TaxModel>> LoadTaxConfigurationsAsync()
    {
        var taxConfigurations =
            await _queryBus.ExecuteAsync<GetTaxesQuery, List<Tax>>(new GetTaxesQuery { ProfileId = TaxProfile.Id });

        return _mapper.Map<ObservableCollection<TaxModel>>(taxConfigurations);
    }

    private async Task SaveTaxConfigurationsExecuteAsync()
    {
        var taxConfigurations = await TaxConfigurations.ToTask();

        //TODO: create "SaveTaxCommand" and handle command status
        foreach (var taxConfiguration in taxConfigurations)
        {
            if (taxConfiguration.Id == Guid.Empty)
            {
                await _commandBus.DispatchAsync(_mapper.Map<CreateTaxCommand>(taxConfiguration));
            }
            else
            {
                await _commandBus.DispatchAsync(_mapper.Map<UpdateTaxCommand>(taxConfiguration));
            }
        }
    }

    private async Task AddTaxConfigurationExecuteAsync()
    {
        var taxConfigurations = await TaxConfigurations.ToTask();
        taxConfigurations.Add(new TaxModel { TaxProfileId = TaxProfile.Id }); 
    }

    private async Task RemoveTaxConfigurationExecuteAsync()
    {
        var result = await _commandBus.DispatchAsync(new RemoveTaxCommand { TaxId = SelectedTaxConfiguration.Id });
        if (result.Status == CommandStatus.Success)
        {
            var taxConfigurations = await TaxConfigurations.ToTask();
            taxConfigurations.Remove(SelectedTaxConfiguration);
        }
    }
}