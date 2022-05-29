using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Dashboard.Queries;

public class GetTaxStatisticsQuery : IQuery
{
    public Guid ProfileId { get; set; }

    public Guid? CurrencyId { get; set; }

    public DateTime? From { get; set; }

    public DateTime? To { get; set; }
}