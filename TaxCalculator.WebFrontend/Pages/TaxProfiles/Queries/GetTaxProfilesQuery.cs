using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Queries;

public class GetTaxProfilesQuery : IQuery
{
    public int? Page { get; set; }

    public int? PageSize { get; set; }
}