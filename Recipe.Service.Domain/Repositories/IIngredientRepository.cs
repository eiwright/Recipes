using Recipe.Service.Domain.Interfaces;

namespace Recipe.Service.Domain.Repositories;

public interface IIngredientRepository : IRepositoryBase<Models.Ingredient, int>
{
    Task<IList<Domain.Models.RecipeIngredients>> GetByRecipeIdAsync(int recipeId);
    Task<IList<Domain.Models.Ingredient>> GetAllAsync();
}