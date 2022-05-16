using TaxCalculator.Domain.Dtos;

namespace TaxCalculator.WebFrontend.Models;

public class CalculateTaxesModel
{
    public List<TaxDataItemDto> TaxInformation { get; set; }

    public IncomeTotalDto? IncomeTotal { get; set; }

    public string? Currency { get; set; }
}