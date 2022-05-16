using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Queries;

public class GetTaxesHandler : IQueryHandler<GetTaxesQuery, List<TaxModel>>
{
    private readonly WebApi _api;

    public GetTaxesHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<List<TaxModel>> HandleAsync(GetTaxesQuery query)
    {
        var taxes = await _api.GetManyAsync<TaxModel>($"Tax?ProfileId={query.ProfileId}");
        return taxes.ToList();
    }
}