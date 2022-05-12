using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Commands;

public class RemoveTaxHandler : ICommandHandler<RemoveTaxCommand>
{
    private readonly WebApi _api;

    public RemoveTaxHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(RemoveTaxCommand command)
    {
        var result = await _api.Remove($"Tax?TaxId={command.TaxId}");
        return result.RecordId.HasValue ? CommandResult.Success() : CommandResult.Fail();
    }
}