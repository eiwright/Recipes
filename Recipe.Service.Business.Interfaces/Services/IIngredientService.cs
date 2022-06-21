using Recipe.Service.Business.Interfaces.DTO;

namespace Recipe.Service.Business.Interfaces.Services;

public interface IIngredientService
{
    Task<IList<DTO.Ingredient>> GetAllIngredientsAsync();
    Task<DTO.Ingredient> AddIngredientAsync(WriteIngredient ingredient);
    Task<DTO.Ingredient> UpdateIngredient(int ingredientId, DTO.Ingredient ingredient);
    Task<DTO.Ingredient> GetIngredientAsync(int id);
}