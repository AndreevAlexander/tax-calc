using TaxCalculator.Domain.Dto;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesResult
{
    public List<TaxDataItemDto> TaxInformation { get; set; } = new();

    public IncomeTotalDto IncomeTotal { get; set; } = new();

    public string? Currency { get; set; }
}