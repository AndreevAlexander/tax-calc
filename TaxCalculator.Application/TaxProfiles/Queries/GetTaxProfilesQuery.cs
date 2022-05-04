using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class GetTaxProfilesQuery : IQuery
{
    public int? Page { get; set; } = null;
    public int? PageSize { get; set; } = null;
}