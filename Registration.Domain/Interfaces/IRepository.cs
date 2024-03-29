﻿using Registration.Domain.Base;
using System.Linq.Expressions;

namespace Registration.Domain.Interfaces;

/// <summary>
/// Adds basic generic methods for all repositories.
/// </summary>
/// <typeparam name="TEntity">Base entity type.</typeparam>
public interface IRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Get entity by id.
    /// </summary>
    /// <param name="id">Entity id.</param>
    /// <returns>Desired entity.</returns>
    TEntity? GetById(long id);

    /// <summary>
    /// Find entities.
    /// </summary>
    /// <param name="predicate">Filter conditions.</param>
    /// <returns>Collection of entities.</returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Find entities.
    /// </summary>
    /// <param name="predicate">Filter conditions.</param>
    /// <param name="paths">Including entities.</param>
    /// <returns>Collection of entities.</returns>
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] paths);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    TEntity Add(TEntity entity);

    /// <summary>
    /// Update existing entity.
    /// </summary>
    /// <param name="entity">Entity to update.</param>
    TEntity Update(TEntity entity);

    /// <summary>
    /// Delete existing entity.
    /// </summary>
    /// <param name="id">Entity id.</param>
    void RemoveById(long id);

}
