using System;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Persistence;

public interface IRepository<TEntity> : IRepository where TEntity : BaseEntity
{
    ValueTask<TEntity?> GetOneAsync(Guid id);

    IQueryable<TEntity> GetMany();
}

public interface IRepository
{
    TRepository As<TRepository>() where TRepository : IRepository;
}