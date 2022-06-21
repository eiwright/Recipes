using Recipe.Service.Domain.Interfaces;
using Recipe.Service.Domain.Models.Search;

namespace Recipe.Service.Domain.Repositories;

public interface IRecipeRepository : IRepositoryBase<Models.Recipe, int>
{
    Task<Models.Recipe> GetByRecipeIdAsync(int recipeId);
    Task<IList<Models.Recipe>> SearchRecipesAsync(string search);
    Task<IList<Models.Recipe>> GetAllAsync();
    Task<PagedSearchResult<Domain.Models.Recipe>> GetPagedResultsAsync(PagedSearch search);
}