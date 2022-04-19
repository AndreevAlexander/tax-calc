using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Taxes.Commands;

public class CreateTaxHandler : ICommandHandler<CreateTaxCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public CreateTaxHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(CreateTaxCommand command)
    {
        var tax = _mapper.Map<Tax>(command);
        
        _entityManager.Persist(tax);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(tax.Id);
    }
}