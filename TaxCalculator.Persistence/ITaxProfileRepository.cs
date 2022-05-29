using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.ValueObjects;

namespace TaxCalculator.Persistence;

public interface ITaxProfileRepository : IRepository<TaxProfile>
{
    ValueTask<TaxProfile?> GetOneAsync(Guid id, DateRange? period = null);
}