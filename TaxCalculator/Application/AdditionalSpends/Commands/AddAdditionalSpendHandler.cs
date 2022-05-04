using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.AdditionalSpends.Commands;

public class AddAdditionalSpendHandler : ICommandHandler<AddAdditionalSpendCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public AddAdditionalSpendHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(AddAdditionalSpendCommand command)
    {
        var additionalSpend = _mapper.Map<AdditionalSpend>(command);
        
        _entityManager.Persist(additionalSpend);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(additionalSpend.Id);
    }
}