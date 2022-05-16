using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.TaxProfiles.Queries;

public class GetTaxProfileByIdQuery : IQuery
{
    public Guid TaxProfileId { get; set; }
}