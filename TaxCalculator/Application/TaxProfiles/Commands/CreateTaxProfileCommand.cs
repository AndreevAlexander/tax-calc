using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.TaxProfiles.Commands;

public class CreateTaxProfileCommand : ICommand
{
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid ProfileCurrencyId { get; set; }
}