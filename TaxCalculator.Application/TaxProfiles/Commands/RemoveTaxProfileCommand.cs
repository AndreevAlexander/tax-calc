using TaxCalculator.Cqrs.Contracts;

namespace TaxCalculator.Application.TaxProfiles.Commands;

public class RemoveTaxProfileCommand : ICommand
{
    public Guid TaxProfileId { get; set; }
}