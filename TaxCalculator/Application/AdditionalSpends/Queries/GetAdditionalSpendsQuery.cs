using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.AdditionalSpends.Queries;

public class GetAdditionalSpendsQuery : IQuery
{
    public Guid? ProfileId { get; set; }

    public int? Page { get; set; }

    public int? PageSize { get; set; }
}