using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Incomes.Commands;

public class UpdateIncomeHandler : ICommandHandler<UpdateIncomeCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public UpdateIncomeHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(UpdateIncomeCommand command)
    {
        var income = await _entityManager.GetRepository<Income>().GetOneAsync(command.IncomeId);

        var result = CommandResult.Fail(command.IncomeId);
        if (income != null)
        {
            income = _mapper.Map(command, income);

            if (command.IncomeDate.HasValue)
            {
                income.IncomeDate = command.IncomeDate.Value;
            }
            
            _entityManager.Persist(income);
            await _entityManager.SaveAsync();
            
            result = CommandResult.Success(command.IncomeId);
        }

        return result;
    }
}