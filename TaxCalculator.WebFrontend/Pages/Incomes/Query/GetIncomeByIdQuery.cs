using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Query;

public class GetIncomeByIdQuery : IQuery
{
    public Guid IncomeId { get; set; }
}