using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Commands;

public class RemoveTaxCommand : ICommand
{
    public Guid TaxId { get; set; }
}