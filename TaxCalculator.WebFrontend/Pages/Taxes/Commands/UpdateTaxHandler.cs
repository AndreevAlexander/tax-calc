using TaxCalculator.Cqrs.Contracts;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Commands;

public class UpdateTaxHandler : ICommandHandler<UpdateTaxCommand>
{
    private readonly WebApi _api;

    public UpdateTaxHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<CommandResult> HandleAsync(UpdateTaxCommand command)
    {
        var result = await _api.Update(command, "Tax");
        return result.RecordId.HasValue ? CommandResult.Success(command.Id) : CommandResult.Fail();
    }
}