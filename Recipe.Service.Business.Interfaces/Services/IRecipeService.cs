using Recipe.Service.Business.Interfaces.DTO;
using Recipe.Service.Business.Interfaces.DTO.Search;
using Recipe.Service.Domain.Models.Search;

namespace Recipe.Service.Business.Interfaces.Services;

public interface IRecipeService
{
    Task<PagedSearchResult<DTO.Recipe>> GetRecipesAsync(RecipeSimpleTermSearch search);
    Task<IList<DTO.Recipe>> SearchRecipesAsync(string search);
    Task<IList<DTO.Recipe>> GetAllRecipesAsync();
    Task<DTO.Recipe> AddRecipeAsync(WriteRecipe recipe);
    Task<DTO.Recipe> AddImageToRecipeAsync(WriteRecipeImage recipe);
    Task<DTO.Recipe> UpdateRecipe(int recipeId, DTO.Recipe recipe);
    Task<DTO.Recipe> GetRecipeAsync(int id);
    Task DeleteAsync(int id);
}