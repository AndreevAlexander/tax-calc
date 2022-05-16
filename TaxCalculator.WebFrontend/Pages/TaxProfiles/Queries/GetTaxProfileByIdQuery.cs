using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Queries;

public class GetTaxProfileByIdQuery : IQuery
{
    public Guid TaxProfileId { get; set; }
}