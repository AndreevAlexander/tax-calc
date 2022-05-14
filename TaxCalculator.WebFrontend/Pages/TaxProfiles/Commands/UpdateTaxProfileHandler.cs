using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Commands;

public class UpdateTaxProfileHandler : ICommandHandler<UpdateTaxProfileCommand>
{
    private readonly WebApi _api;

    public UpdateTaxProfileHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(UpdateTaxProfileCommand command)
    {
        var result = await _api.Update(command, "TaxProfile");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId) : CommandResult.Fail();
    }
}