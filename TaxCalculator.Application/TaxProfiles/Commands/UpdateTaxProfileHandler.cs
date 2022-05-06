using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.TaxProfiles.Commands;

public class UpdateTaxProfileHandler : ICommandHandler<UpdateTaxProfileCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public UpdateTaxProfileHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(UpdateTaxProfileCommand command)
    {
        var result = CommandResult.Fail(command.TaxProfileId);
        
        var profile = await _entityManager.GetRepository<TaxProfile>().GetOneAsync(command.TaxProfileId);
        if (profile != null)
        {
            profile = _mapper.Map(command, profile);
            
            _entityManager.Persist(profile);
            await _entityManager.SaveAsync();

            result.Status = CommandStatus.Success;
        }

        return result;
    }
}