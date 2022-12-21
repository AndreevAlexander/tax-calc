using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Cqrs.Contracts.Handler;
using TaxCalculator.UI.Data.Contracts;

namespace TaxCalculator.UI.Desktop.Views.TaxProfiles.Queries
{
    public class GetTaxProfilesQueryHandler : IQueryHandler<GetTaxProfilesQuery, List<TaxProfileModel>>
    {
        private readonly IWebApi _webApi;

        public GetTaxProfilesQueryHandler(IWebApi webApi)
        {
            _webApi = webApi;
        }

        public async Task<List<TaxProfileModel>> HandleAsync(GetTaxProfilesQuery query)
        {
            var models = await _webApi.GetManyAsync<TaxProfileModel>("TaxProfile");
            return models.ToList();
        }
    }
}
