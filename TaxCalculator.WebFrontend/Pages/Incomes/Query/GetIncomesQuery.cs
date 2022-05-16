using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Query;

public class GetIncomesQuery : IQuery
{
    public Guid TaxProfileId { get; set; }
}