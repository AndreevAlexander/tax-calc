using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.AdditionalSpends.Commands;

public class RemoveAdditionalSpendHandler : ICommandHandler<RemoveAdditionalSpendCommand>
{
    private readonly IEntityManager _entityManager;

    public RemoveAdditionalSpendHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<CommandResult> HandleAsync(RemoveAdditionalSpendCommand command)
    {
        _entityManager.Remove<AdditionalSpend>(command.AdditionalSpendId);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(command.AdditionalSpendId);
    }
}