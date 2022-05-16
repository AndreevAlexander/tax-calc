using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Commands;

public class UpdateTaxProfileCommand : ICommand
{
    public Guid TaxProfileId { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
}