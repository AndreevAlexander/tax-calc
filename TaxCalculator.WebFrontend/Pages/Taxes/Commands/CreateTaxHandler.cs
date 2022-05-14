using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Commands;

public class CreateTaxHandler : ICommandHandler<CreateTaxCommand>
{
    private readonly WebApi _api;

    public CreateTaxHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(CreateTaxCommand command)
    {
        var result = await _api.Create(command, "Tax");
        return result.RecordId.HasValue ? CommandResult.Success(result.RecordId) : CommandResult.Fail();
    }
}