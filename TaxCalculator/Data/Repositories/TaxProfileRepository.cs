using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.ValueObjects;
using TaxCalculator.Persistence;

namespace TaxCalculator.Data.Repositories;

public class TaxProfileRepository : RepositoryBase<TaxProfile>, ITaxProfileRepository
{
    public TaxProfileRepository(TaxContext context) : base(context)
    {
    }

    public async ValueTask<TaxProfile?> GetOneAsync(Guid id, DateRange? period = null)
    {
        var taxProfile = await base.GetOneAsync(id);

        if (taxProfile != null)
        {
            var incomes = Context.Incomes.Where(x => x.TaxProfileId == taxProfile.Id);
            var additionalSpends = Context.AdditionalSpends.Where(x => x.TaxProfileId == taxProfile.Id);
            var taxes = Context.Taxes.Where(x => x.TaxProfileId == taxProfile.Id);

            if (period != null)
            {
                incomes = incomes.Where(x => x.CreatedDate >= period.From && x.CreatedDate <= period.To);
                additionalSpends = additionalSpends.Where(x => x.CreatedDate >= period.From && x.CreatedDate <= period.To);
                taxes = taxes.Where(x => x.CreatedDate >= period.From && x.CreatedDate <= period.To);
            }

            taxProfile.AdditionalSpends = additionalSpends.ToList();
            taxProfile.Incomes = incomes.ToList();
            taxProfile.Taxes = taxes.ToList();
            taxProfile.ProfileCurrency = Context.Currencies.FirstOrDefault(x => x.Id == taxProfile.ProfileCurrencyId);
        }

        return taxProfile;
    }
}