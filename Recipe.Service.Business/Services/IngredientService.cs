using Recipe.Service.Business.Interfaces.DTO;
using Recipe.Service.Business.Interfaces.Services;
using Recipe.Service.Business.Mappers;
using Recipe.Service.Domain.Repositories;
using Recipe.Service.Business.Interfaces.Exceptions;

namespace Recipe.Service.Business.Services;

public class IngredientService : IIngredientService
{
    private IIngredientRepository IngredientRepository { get; set; }

    public IngredientService(IIngredientRepository ingredientRepository)
    {
        IngredientRepository = ingredientRepository;
    }


    public async Task<IList<Interfaces.DTO.Ingredient>> GetAllIngredientsAsync()
    {
        var result = await IngredientRepository.GetAllAsync();
        return result.Select(x => x.ToDTO()).ToList();
    }

    public async Task<Interfaces.DTO.Ingredient> AddIngredientAsync(WriteIngredient ingredient)
    {
        var dbIngredient = ingredient?.ToDomain();
        if (dbIngredient == null)
        {
            throw new Exception("Ingredient invalid");
        }

        var newIngredient = await IngredientRepository.AddAsync(dbIngredient);
        return newIngredient.ToDTO();
    }

    public async Task<Interfaces.DTO.Ingredient> GetIngredientAsync(int id)
    {
        var ingredient = await IngredientRepository.GetByIdAsync(id);
        return ingredient.ToDTO();
    }

    public async Task<Interfaces.DTO.Ingredient> UpdateIngredient(int ingredientId, Interfaces.DTO.Ingredient ingredient)
    {
        var dbIngredient = await IngredientRepository.GetByIdAsync(ingredientId);
        if (dbIngredient == default)
        {
            throw new NotFoundException("Ingredient not found");
        }

        dbIngredient.Name = ingredient.Name ?? dbIngredient.Name;
        var result = await IngredientRepository.UpdateAsync(dbIngredient);
        return result.ToDTO();
    }
}
