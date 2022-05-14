using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Commands;

public class CreateAdditionalSpendHandler : ICommandHandler<CreateAdditionalSpendCommand>
{
    private readonly WebApi _api;

    public CreateAdditionalSpendHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(CreateAdditionalSpendCommand command)
    {
        var result = await _api.Create(command, "AdditionalSpend");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId) : CommandResult.Fail();
    }
}