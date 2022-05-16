using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Queries;

public class GetTaxByIdQuery : IQuery
{
    public Guid TaxId { get; set; }
}