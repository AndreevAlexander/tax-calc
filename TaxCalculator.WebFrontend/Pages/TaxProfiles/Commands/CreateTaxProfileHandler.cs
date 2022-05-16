using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.TaxProfiles.Commands;

public class CreateTaxProfileHandler : ICommandHandler<CreateTaxProfileCommand>
{
    private readonly WebApi _api;

    public CreateTaxProfileHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(CreateTaxProfileCommand command)
    {
        var result = await _api.Create(command, "TaxProfile");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId.Value) : CommandResult.Fail();
    }
}