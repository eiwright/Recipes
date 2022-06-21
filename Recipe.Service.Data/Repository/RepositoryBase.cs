
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Recipe.Service.Data.Extensions;
using Recipe.Service.Domain.Entities;
using Recipe.Service.Domain.Interfaces;

namespace Recipe.Service.Data.Repository;

public abstract class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId> where TEntity : BaseEntity<TId>
{
    protected DataContext DataContext { get; }

    protected RepositoryBase(DataContext dataContext)
    {
        DataContext = dataContext;
    }

    protected abstract IQueryable<TEntity> GetQueryableById(TId id);
    protected abstract IQueryable<TEntity> GetQueryableByIds(IEnumerable<TId> ids);

    public async Task<IList<TEntity>> AddAsync(IEnumerable<TEntity> models) => await AddAsync(models.ToArray());

    public virtual async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includedProperties) =>
        await GetQueryableById(id)
            .AddIncludedProperties(includedProperties)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);

    public async Task<IList<TEntity>> GetByIdsAsync(IEnumerable<TId> ids, params Expression<Func<TEntity, object>>[] includedProperties) =>
        await GetQueryableByIds(ids)
            .AddIncludedProperties(includedProperties)
            .ToListAsync()
            .ConfigureAwait(false);

    public virtual async Task<TEntity> AddAsync(TEntity model)
    {
        DataContext.Add(model);
        await DataContext.SaveChangesAsync();
        return model;
    }

    public virtual async Task<IList<TEntity>> AddAsync(params TEntity[] models)
    {
        DataContext.AddRange(models.Cast<object>());
        await DataContext.SaveChangesAsync();
        return models;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity model)
    {
        DataContext.Update(model);
        await DataContext.SaveChangesAsync();
        return model;
    }

    public async Task<IList<TEntity>> UpdateAsync(IEnumerable<TEntity> models)
    {
        var modelsList = models.ToList();
        DataContext.UpdateRange(modelsList);
        await DataContext.SaveChangesAsync();
        return modelsList;
    }

    public virtual async Task UpdateAsync()
    {
        await DataContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TId id)
    {
        var model = await GetByIdAsync(id).ConfigureAwait(false);
        switch (model)
        {
            case null:
                return;
            default:
                DataContext.Remove(model);
                break;
        }

        await DataContext.SaveChangesAsync();
    }

    protected static IOrderedQueryable<TSource> CreateSortQuery<TSource, TKey>(IQueryable<TSource> query, Expression<Func<TSource, TKey>> orderByFunc, bool sortAsc) =>
        sortAsc ? query.OrderBy(orderByFunc) : query.OrderByDescending(orderByFunc);


}
