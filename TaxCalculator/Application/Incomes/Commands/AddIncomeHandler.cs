using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Incomes.Commands;

public class AddIncomeHandler : ICommandHandler<AddIncomeCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public AddIncomeHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(AddIncomeCommand command)
    {
        var income = _mapper.Map<Income>(command);
        
        _entityManager.Persist(income);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(income.Id);
    }
}