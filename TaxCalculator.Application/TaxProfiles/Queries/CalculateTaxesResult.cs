using System.Collections.Generic;
using TaxCalculator.Domain.Dtos;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesResult
{
    public List<TaxDataItemDto> TaxInformation { get; set; }

    public IncomeTotalDto IncomeTotal { get; set; }

    public string? Currency { get; set; }
}