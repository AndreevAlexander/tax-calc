using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Queries;

public class GetAdditionalSpendByIdQuery : IQuery
{
    public Guid AdditionalSpendId { get; set; }
}