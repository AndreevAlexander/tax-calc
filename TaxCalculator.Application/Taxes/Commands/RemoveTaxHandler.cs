using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Taxes.Commands;

public class RemoveTaxHandler : ICommandHandler<RemoveTaxCommand>
{
    private readonly IEntityManager _entityManager;

    public RemoveTaxHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<CommandResult> HandleAsync(RemoveTaxCommand command)
    {
        _entityManager.Remove<Tax>(command.TaxId);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(command.TaxId);
    }
}