using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.Taxes.Commands;

public class RemoveTaxCommand : ICommand
{
    public Guid TaxId { get; set; }
}