using TaxCalculator.Domain.Dtos;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class CalculateTaxesResult
{
    public List<TaxDataItemDto> TaxInformation { get; set; }

    public TaxTotalDto TaxTotal { get; set; }
}