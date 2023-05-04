using System;

namespace TaxCalculator.Desktop.Models;
public class CurrencyModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public double ExchangeRate { get; set; }
}