using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Queries;

public class GetAdditionalSpendByIdHandler : IQueryHandler<GetAdditionalSpendByIdQuery, AdditionalSpendModel>
{
    private readonly WebApi _api;

    public GetAdditionalSpendByIdHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<AdditionalSpendModel> HandleAsync(GetAdditionalSpendByIdQuery query)
    {
        var spend = await _api.GetOneAsync<AdditionalSpendModel>($"AdditionalSpend/one?AdditionalSpendId={query.AdditionalSpendId}");
        return spend;
    }
}