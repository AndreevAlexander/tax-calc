using TaxCalculator.Contracts;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Data.Repositories;

public class CurrencyRepository : RepositoryBase<Currency>
{
    private readonly ICache _cache;

    public CurrencyRepository(TaxContext context, ICache cache) : base(context)
    {
        _cache = cache;
    }

    public override IQueryable<Currency> GetMany()
    {
        return _cache.GetSet("currencies", () => base.GetMany().ToList().AsQueryable());
    }
}