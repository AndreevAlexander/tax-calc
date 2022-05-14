using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Commands;

public class RemoveAdditionalSpendHandler : ICommandHandler<RemoveAdditionalSpendCommand>
{
    private readonly WebApi _api;

    public RemoveAdditionalSpendHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(RemoveAdditionalSpendCommand command)
    {
        var result = await _api.Remove($"AdditionalSpend?AdditionalSpendId={command.AdditionalSpendId}");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId) : CommandResult.Fail();
    }
}