using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Taxes.Queries;

public class GetTaxByIdQuery : IQuery
{
    public Guid TaxId { get; set; }
}