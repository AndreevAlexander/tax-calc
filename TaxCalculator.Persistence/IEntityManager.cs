using System.Threading.Tasks;

namespace TaxCalculator.Persistence;

public interface IEntityManager
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    
    void Persist<TEntity>(TEntity entity) where TEntity : class;
    
    void Remove<TEntity>(Guid id) where TEntity : class;
    
    Task SaveAsync();
}