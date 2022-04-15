using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Data.Repositories;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly TaxContext _context;

    public RepositoryBase(TaxContext context)
    {
        _context = context;
    }

    public ValueTask<TEntity?> GetOneAsync(Guid id)
    {
        return _context.Set<TEntity>().FindAsync(id);
    }

    public IQueryable<TEntity> GetMany()
    {
        return _context.Set<TEntity>().AsQueryable();
    }
}