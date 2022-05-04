using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.AdditionalSpends.Queries;

public class GetAdditionalSpendByIdQuery : IQuery
{
    public Guid AdditionalSpendId { get; set; }
}