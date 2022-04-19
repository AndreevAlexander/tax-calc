﻿using TaxCalculator.Domain.Entities;
using TaxCalculator.Persistence;

namespace TaxCalculator.Data.Repositories;

public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly TaxContext Context;

    public RepositoryBase(TaxContext context)
    {
        Context = context;
    }

    public virtual ValueTask<TEntity?> GetOneAsync(Guid id)
    {
        return Context.Set<TEntity>().FindAsync(id);
    }

    public virtual IQueryable<TEntity> GetMany()
    {
        return Context.Set<TEntity>().AsQueryable();
    }

    public TRepository As<TRepository>() where TRepository : IRepository
    {
        if (this is TRepository repository)
        {
            return repository;
        }

        throw new Exception("Can not cast repository");
    }
}