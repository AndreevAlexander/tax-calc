using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Incomes.Commands;

public class UpdateIncomeCommand : ICommand
{
    public Guid IncomeId { get; set; }
    
    public decimal Value { get; set; }

    public DateTime? IncomeDate { get; set; }
}