﻿using TaxCalculator.Data.Repositories;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Data;

public class EntityManager : IEntityManager
{
    private readonly TaxContext _context;

    private readonly Dictionary<Type, IRepository> _repositories;

    public EntityManager(TaxContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, IRepository>();
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