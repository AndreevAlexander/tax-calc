using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Commands;

public class CreateTaxProfileCommand : ICommand
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid ProfileCurrencyId { get; set; }
}