using System.Threading.Tasks;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Persistence;

public interface IEntityManager
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    void Persist<TEntity>(TEntity entity) where TEntity : BaseEntity;
    Task SaveAsync();
}