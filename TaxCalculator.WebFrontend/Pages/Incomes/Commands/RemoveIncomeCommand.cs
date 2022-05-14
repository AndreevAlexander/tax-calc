using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Incomes.Commands;

public class RemoveIncomeCommand : ICommand
{
    public Guid IncomeId { get; set; }
}