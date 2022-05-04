using System;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Domain.Services;

public interface ICurrencyConverterService
{
    decimal ToSystem(decimal value, TaxProfile taxProfile);
    decimal ToCurrency(decimal value, TaxProfile taxProfile, Guid currencyId);
}