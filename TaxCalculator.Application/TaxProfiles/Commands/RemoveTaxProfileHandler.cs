using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Commands;

public class RemoveTaxProfileHandler : ICommandHandler<RemoveTaxProfileCommand>
{
    private readonly IEntityManager _entityManager;

    public RemoveTaxProfileHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<CommandResult> HandleAsync(RemoveTaxProfileCommand command)
    {
        _entityManager.Remove<TaxProfile>(command.TaxProfileId);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(command.TaxProfileId);
    }
}