using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Commands;

public class UpdateAdditionalSpendHandler : ICommandHandler<UpdateAdditionalSpendCommand>
{
    private readonly WebApi _api;

    public UpdateAdditionalSpendHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(UpdateAdditionalSpendCommand command)
    {
        var result = await _api.Update(command, "AdditionalSpend");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId) : CommandResult.Fail();
    }
}