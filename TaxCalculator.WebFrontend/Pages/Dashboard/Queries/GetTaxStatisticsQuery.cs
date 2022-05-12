using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Dashboard.Queries;

public class GetTaxStatisticsQuery : IQuery
{
    public Guid ProfileId { get; set; }
}