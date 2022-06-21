using Recipe.Service.Domain.Entities;
using System.Linq.Expressions;

namespace Recipe.Service.Domain.Interfaces;

public interface IRepositoryBase<TModel, in TId> where TModel : BaseEntity<TId>
{
    Task<TModel> AddAsync(TModel model);
    Task<IList<TModel>> AddAsync(params TModel[] models);
    Task<IList<TModel>> AddAsync(IEnumerable<TModel> models);
    Task<TModel> GetByIdAsync(TId id, params Expression<Func<TModel, object>>[] includedProperties);
    Task<IList<TModel>> GetByIdsAsync(IEnumerable<TId> ids, params Expression<Func<TModel, object>>[] includedProperties);
    Task DeleteAsync(TId id);
    Task<TModel> UpdateAsync(TModel model);
    Task<IList<TModel>> UpdateAsync(IEnumerable<TModel> models);
    Task UpdateAsync();
}
