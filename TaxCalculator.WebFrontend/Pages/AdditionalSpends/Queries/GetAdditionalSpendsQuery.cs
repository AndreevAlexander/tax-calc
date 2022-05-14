using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Queries;

public class GetAdditionalSpendsQuery : IQuery
{
    public Guid TaxProfileId { get; set; }

    public int? Page { get; set; }
    
    public int? PageSize { get; set; }
}