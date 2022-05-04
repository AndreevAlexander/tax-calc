using System.Threading.Tasks;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Commands;

public class CreateTaxProfileHandler : ICommandHandler<CreateTaxProfileCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public CreateTaxProfileHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(CreateTaxProfileCommand command)
    {
        var entity = _mapper.Map<TaxProfile>(command);
        _entityManager.Persist(entity);
        await _entityManager.SaveAsync();
        
        return CommandResult.Success(entity.Id);
    }
}