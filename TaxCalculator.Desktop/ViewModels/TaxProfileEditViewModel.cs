using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using ReactiveUI;
using TaxCalculator.Application.Taxes.Queries;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts.Bus;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Desktop.ViewModels.BaseTypes;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Services.Identifier;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileEditViewModel : RoutedViewModel
{
    private readonly IQueryBus _queryBus;
    
    private readonly IMapper _mapper;

    private KeyValuePair<Guid, string> _selectedCurrency;

    public TaxProfileModel TaxProfile { get; set; }

    public KeyValuePair<Guid, string> SelectedCurrency
    {
        get => _selectedCurrency;
        set => this.RaiseAndSetIfChanged(ref _selectedCurrency, value);
    }

    public Dictionary<Guid, string> Currencies { get; }

    public IObservable<ObservableCollection<TaxModel>> TaxConfigurations { get; set; }
    
    public TaxProfileEditViewModel(IIdentifierService identifierService,
                                   IQueryBus queryBus,
                                   IMapper mapper)
    {
        _queryBus = queryBus;
        _mapper = mapper;
        Currencies = identifierService.Currencies.ToDictionary();
    }

    public override async void OnBeforeNavigated(object parameter)
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
}