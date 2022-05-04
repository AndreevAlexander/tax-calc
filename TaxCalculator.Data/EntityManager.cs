using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Contracts;
using TaxCalculator.Data.Repositories;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Data;

public class EntityManager : IEntityManager
{
    private readonly TaxContext _context;

    private readonly Dictionary<Type, IRepository> _repositories;

    public EntityManager(TaxContext context, ICache cache)
    {
        _context = context;
        _repositories = new Dictionary<Type, IRepository>
        {
            {typeof(TaxProfile), new TaxProfileRepository(_context)},
            {typeof(Currency), new CurrencyRepository(_context, cache)}
        };
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
    {
        var notExists = !_repositories.TryGetValue(typeof(TEntity), out IRepository? repository);
        if (notExists)
        {
            repository = new RepositoryBase<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
        }

        return (IRepository<TEntity>)repository;
    }

    public void Persist<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        var isTracked = _context.ChangeTracker.Entries().Any(e => e.Entity == entity);
        if (isTracked)
        {
            entity.UpdatedDate = DateTime.Now;
        }
        else
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedDate = DateTime.Now;

            _context.Set<TEntity>().Add(entity);
        }
    }

    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}