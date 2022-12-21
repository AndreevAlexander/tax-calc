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

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var notExists = !_repositories.TryGetValue(typeof(TEntity), out IRepository? repository);
        if (notExists)
        {
            repository = new RepositoryBase<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
        }

        return (IRepository<TEntity>)repository;
    }

    public void Persist<TEntity>(TEntity entity) where TEntity : class
    {
        var isTracked = _context.ChangeTracker.Entries().Any(e => e.Entity == entity);
        if (isTracked)
        {
            ((dynamic)entity).UpdatedDate = DateTime.Now;
        }
        else
        {
            ((dynamic)entity).Id = Guid.NewGuid();
            ((dynamic)entity).CreatedDate = DateTime.Now;

            _context.Set<TEntity>().Add(entity);
        }
    }

    public void Remove<TEntity>(Guid id) where TEntity : class
    {
        var idProperty = typeof(TEntity).GetProperty(nameof(BaseEntity.Id));

        var entity = GetRepository<TEntity>().GetMany().FirstOrDefault(x => idProperty.GetValue(x).Equals(id));
        if (entity != null)
        {
            _context.Remove(entity);
        }
    }

    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}