using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Persistence;

public interface IRepository<TEntity> : IRepository where TEntity : BaseEntity
{
    ValueTask<TEntity?> GetOneAsync(Guid id);

    IQueryable<TEntity> GetMany();
}

public interface IRepository
{
    
}