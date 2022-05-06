using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Incomes.Commands;

public class RemoveIncomeHandler : ICommandHandler<RemoveIncomeCommand>
{
    private readonly IEntityManager _entityManager;

    public RemoveIncomeHandler(IEntityManager entityManager)
    {
        _entityManager = entityManager;
    }

    public async Task<CommandResult> HandleAsync(RemoveIncomeCommand command)
    {
        _entityManager.Remove<Income>(command.IncomeId);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(command.IncomeId);
    }
}