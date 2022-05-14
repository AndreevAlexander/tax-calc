using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.WebFrontend.Data;
using TaxCalculator.WebFrontend.Models;

namespace TaxCalculator.WebFrontend.Pages.AdditionalSpends.Queries;

public class GetAdditionalSpendsHandler : IQueryHandler<GetAdditionalSpendsQuery, List<AdditionalSpendModel>>
{
    private readonly WebApi _api;

    public GetAdditionalSpendsHandler(WebApi api)
    {
        _api = api;
    }
    
    public async Task<List<AdditionalSpendModel>> HandleAsync(GetAdditionalSpendsQuery query)
    {
        var url = $"AdditionalSpend?ProfileId={query.TaxProfileId}";

        if (query.Page.HasValue && query.PageSize.HasValue)
        {
            url = $"{url}&Page={query.Page}&PageSize={query.PageSize}";
        }
        
        var spends = await _api.GetManyAsync<AdditionalSpendModel>(url);
        return spends.ToList();
    }
}