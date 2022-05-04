using System.Threading.Tasks;
using TaxCalculator.Contracts;
using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Application.Taxes.Commands;

public class UpdateTaxHandler : ICommandHandler<UpdateTaxCommand>
{
    private readonly IEntityManager _entityManager;
    private readonly IMapper _mapper;

    public UpdateTaxHandler(IEntityManager entityManager, IMapper mapper)
    {
        _entityManager = entityManager;
        _mapper = mapper;
    }

    public async Task<CommandResult> HandleAsync(UpdateTaxCommand command)
    {
        var tax = await _entityManager.GetRepository<Tax>().GetOneAsync(command.Id);

        var result = CommandResult.Fail();
        if (tax != null)
        {
            tax = _mapper.Map(command, tax);
        
            _entityManager.Persist(tax);
            await _entityManager.SaveAsync();
            
            result = CommandResult.Success(command.Id);
        }
        
        return result;
    }
}