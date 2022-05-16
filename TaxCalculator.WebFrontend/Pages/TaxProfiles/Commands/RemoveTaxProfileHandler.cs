using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Commands;

public class RemoveTaxProfileHandler : ICommandHandler<RemoveTaxProfileCommand>
{
    private readonly WebApi _api;

    public RemoveTaxProfileHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(RemoveTaxProfileCommand command)
    {
        var result = await _api.Remove($"TaxProfile?TaxProfileId={command.ProfileId}");

        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId.Value) : CommandResult.Fail();
    }
}