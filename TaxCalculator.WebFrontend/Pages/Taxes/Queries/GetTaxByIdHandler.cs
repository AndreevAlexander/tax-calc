using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.Taxes.Queries;

public class GetTaxByIdHandler : IQueryHandler<GetTaxByIdQuery, TaxModel>
{
    private readonly WebApi _api;

    public GetTaxByIdHandler(WebApi api)
    {
        _api = api;
    }

    public async Task<TaxModel> HandleAsync(GetTaxByIdQuery query)
    {
        var tax = await _api.GetOneAsync<TaxModel>($"Tax/one?TaxId={query.TaxId}");
        return tax;
    }
}