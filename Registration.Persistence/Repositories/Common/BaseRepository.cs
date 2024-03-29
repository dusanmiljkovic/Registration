﻿using Registration.Domain.Base;
using Registration.Domain.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Registration.Persistence.Data;
using Registration.Shared.Extensions;

namespace Registration.Persistence.Repositories.Common;

/// <summary>
/// BaseRepository class.
/// </summary>
/// <typeparam name="TEntity">Base entity type.</typeparam>
public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    #region Fields

    /// <summary>
    /// Database context.
    /// </summary>
    private readonly RegistrationContext _context;

    /// <summary>
    /// Generic database set.
    /// </summary>
    private readonly DbSet<TEntity> _dbSet;

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseRepository"/> class.
    /// </summary>
    public BaseRepository(RegistrationContext context)
    {
        _context = context.NotNull(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    /// <inheritdoc/>
    public TEntity Add(TEntity entity)
    {
        return _dbSet.Add(entity).Entity;
    }

    /// <inheritdoc/>
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbSet.Where(predicate);
    }
    
    /// <inheritdoc/>
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] paths)
    {
        var query = _dbSet.Where(predicate);
        foreach (var path in paths)
        {
            query = query.Include(path);
        }

        return query;
    }

    /// <inheritdoc/>
    public TEntity? GetById(long id)
    {
        return _dbSet.Find(id);
    }

    /// <inheritdoc/>
    public void RemoveById(long id)
    {
        TEntity? entity = _dbSet.Find(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
        }
    }

    /// <inheritdoc/>
    public TEntity Update(TEntity entityToUpdate)
    {
        _dbSet.Attach(entityToUpdate);
        _context.Entry(entityToUpdate).State = EntityState.Modified;
        return entityToUpdate;
    }
}
