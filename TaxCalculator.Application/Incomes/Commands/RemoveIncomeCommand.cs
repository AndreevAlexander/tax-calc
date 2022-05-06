using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Incomes.Commands;

public class RemoveIncomeCommand : ICommand
{
    public Guid IncomeId { get; set; }
}