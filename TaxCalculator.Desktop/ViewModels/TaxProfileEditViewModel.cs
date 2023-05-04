using System;
using System.Collections.Generic;
using ReactiveUI;
using TaxCalculator.Desktop.Models;
using TaxCalculator.Desktop.ViewModels.BaseTypes;
using TaxCalculator.Domain.Services.Identifier;

namespace TaxCalculator.Desktop.ViewModels;

public class TaxProfileEditViewModel : RoutedViewModel
{
    private KeyValuePair<Guid, string> _selectedCurrency;

    public TaxProfileModel TaxProfile { get; set; }

    public KeyValuePair<Guid, string> SelectedCurrency
    {
        get => _selectedCurrency;
        set => this.RaiseAndSetIfChanged(ref _selectedCurrency, value);
    }

    public Dictionary<Guid, string> Currencies { get; }

    public TaxProfileEditViewModel(IIdentifierService identifierService)
    {
        Currencies = identifierService.Currencies.ToDictionary();
    }

    public override void OnBeforeNavigated(object parameter)
    {
        if (parameter is TaxProfileModel model)
        {
            TaxProfile = model;
            SelectedCurrency = new KeyValuePair<Guid, string>(model.Currency.Id, model.Currency.Name);
        }
    }
}