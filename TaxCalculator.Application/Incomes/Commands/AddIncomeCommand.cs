using System;
using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Incomes.Commands;

public class AddIncomeCommand : ICommand
{
    public decimal Value { get; set; }
    
    public Guid TaxProfileId { get; set; }

    public DateTime IncomeDate { get; set; }
}