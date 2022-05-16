using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Queries;

public class GetTaxesQuery : IQuery
{
    public Guid ProfileId { get; set; }
    
    public int? Page { get; set; }
    
    public int? PageSize { get; set; }
}