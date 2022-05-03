using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Incomes.Queries;

public class GetIncomesQuery : IQuery
{
    public Guid? ProfileId { get; set; }

    public int? Page { get; set; }

    public int? PageSize { get; set; }
}