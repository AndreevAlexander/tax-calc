using System;
using System.Linq;
using System.Threading.Tasks;
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
            var incomes = Context.Incomes.Where(x => x.TaxProfileId == taxProfile.Id && x.Value > 0);
            var additionalSpends = Context.AdditionalSpends.Where(x => x.TaxProfileId == taxProfile.Id && x.Amount > 0);
            var taxes = Context.Taxes.Where(x => x.TaxProfileId == taxProfile.Id && x.Amount > 0);

            if (period != null)
            {
                incomes = incomes.Where(x => x.IncomeDate >= period.From && x.IncomeDate <= period.To);
                additionalSpends = additionalSpends.Where(x => x.CreatedDate >= period.From && x.CreatedDate <= period.To);
            }

            taxProfile.AdditionalSpends = additionalSpends.ToList();
            taxProfile.Incomes = incomes.ToList();
            taxProfile.Taxes = taxes.ToList();
            taxProfile.ProfileCurrency = Context.Currencies.FirstOrDefault(x => x.Id == taxProfile.ProfileCurrencyId);
        }

        return taxProfile;
    }
} 