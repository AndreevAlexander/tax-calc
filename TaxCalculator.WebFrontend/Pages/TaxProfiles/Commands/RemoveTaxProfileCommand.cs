using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Commands;

public class RemoveTaxProfileCommand : ICommand
{
    public Guid ProfileId { get; set; }
}