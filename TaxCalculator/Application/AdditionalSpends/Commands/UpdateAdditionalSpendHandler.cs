using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.AdditionalSpends.Commands;

public class UpdateAdditionalSpendHandler : ICommandHandler<UpdateAdditionalSpendCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public UpdateAdditionalSpendHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(UpdateAdditionalSpendCommand command)
    {
        var additionalSpend = await _entityManager.GetRepository<AdditionalSpend>()
            .GetOneAsync(command.AdditionalSpendId);

        var result = CommandResult.Fail(command.AdditionalSpendId);
        if (additionalSpend != null)
        {
            additionalSpend = _mapper.Map(command, additionalSpend);
            
            _entityManager.Persist(additionalSpend);
            await _entityManager.SaveAsync();
            
            result = CommandResult.Success(command.AdditionalSpendId);
        }

        return result;
    }
}