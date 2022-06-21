using Recipe.Service.Business.Interfaces.DTO;
using Recipe.Service.Business.Interfaces.DTO.Search;
using Recipe.Service.Business.Interfaces.Services;
using Recipe.Service.Business.Mappers;
using Recipe.Service.Domain.Models.Search;
using Recipe.Service.Domain.Repositories;
using Recipe.Service.Business.Interfaces.Exceptions;
using Recipe.Service.Business.Utils;

namespace Recipe.Service.Business.Services;

public class RecipeService : IRecipeService
{
    private IRecipeRepository RecipeRepository { get; set; }
    private IInstructionRepository InstructionRepository { get; set; }
    private IIngredientRepository IngredientRepository { get; set; }

    public RecipeService(IRecipeRepository recipeRepository, 
        IInstructionRepository instructionRepository,
        IIngredientRepository ingredientRepository)
    {
        RecipeRepository = recipeRepository;
        InstructionRepository = instructionRepository;
        IngredientRepository = ingredientRepository;
    }


    public async Task<IList<Interfaces.DTO.Recipe>> GetAllRecipesAsync()
    {
        var result = await RecipeRepository.GetAllAsync();
        return result.Select(x => x.ToDTO()).ToList();
    }

    public async Task<IList<Interfaces.DTO.Recipe>> SearchRecipesAsync(string search)
    {
        var result = await RecipeRepository.SearchRecipesAsync(search);
        return result.Select(x => x.ToDTO()).ToList();
    }

    public async Task<Interfaces.DTO.Recipe> AddRecipeAsync(WriteRecipe recipe)
    {
        var dbRecipe = recipe?.ToDomain();
        if (dbRecipe == null)
        {
            throw new Exception("Recipe invalid");
        }

        var instructions = recipe.Instructions.Select(x => x.ToDomain()).ToList();
        dbRecipe.Instructions.AddRange(instructions);

        var newRecipe = await RecipeRepository.AddAsync(dbRecipe);
        newRecipe.Ingredients.AddRange(recipe.Ingredients.Select(x =>
            new Domain.Models.RecipeIngredients { 
                IngredientId = x.Id,
                Ingredient = new Domain.Models.Ingredient { Id = x.Id, Name = x.Name },
                RecipeId = newRecipe.Id, 
                Quantity = x.Quantity ?? "", 
                Calories = x.Calories 
            }));

        newRecipe = await RecipeRepository.UpdateAsync(newRecipe);

        return newRecipe.ToDTO();
    }

    public async Task<Interfaces.DTO.Recipe> AddImageToRecipeAsync(WriteRecipeImage recipeImage)
    {
        var dbRecipe = await RecipeRepository.GetByRecipeIdAsync(recipeImage.Id);
        if (dbRecipe == null)
        {
            throw new Exception("Recipe invalid");
        }
        var (fileName, bytes) = ByteArrayHelper.ImageToByteArray(recipeImage.ImageData);

        dbRecipe.ImageFileName = fileName ?? dbRecipe.ImageFileName;
        dbRecipe.ImageData = bytes ?? dbRecipe.ImageData;
        var updatedRecipe = await RecipeRepository.UpdateAsync(dbRecipe);

        return updatedRecipe.ToDTO();
    }
    
    public async Task<Interfaces.DTO.Recipe> GetRecipeAsync(int id)
    {
        var recipe = await RecipeRepository.GetByRecipeIdAsync(id);
        return recipe.ToDTO();
    }

    public async Task<PagedSearchResult<Interfaces.DTO.Recipe>> GetRecipesAsync(RecipeSimpleTermSearch search)
    {
        var result = await RecipeRepository.GetPagedResultsAsync(
            new PagedSearch
            {
                PageNumber = search.PageOffset,
                PageSize = search.PageSize,
                SearchString = search.SearchString,
                SortAsc = search.SortAsc,
                SortField = search.SortField
            });

        return new PagedSearchResult<Interfaces.DTO.Recipe>
        {
            Records = result.Records.Select(x => x.ToDTO()).ToList(),
            PageNumber = result.PageNumber,
            PageSize = result.PageSize,
            TotalRecords = result.TotalRecords,
        };
    }

    public async Task<Interfaces.DTO.Recipe> UpdateRecipe(int recipeId, Interfaces.DTO.Recipe recipe)
    {
        var dbRecipe = await RecipeRepository.GetByRecipeIdAsync(recipeId);
        if (dbRecipe == default)
        {
            throw new NotFoundException("Recipe not found");
        }

        if (recipe.Instructions.Any())
        {
            dbRecipe.Instructions = recipe.Instructions.Select(x => x.ToDomain()).ToList();
        }
        if (recipe.Ingredients.Any())
        {
            dbRecipe.Ingredients = recipe.Ingredients?.Select(x => 
                new Domain.Models.RecipeIngredients{ 
                    IngredientId = x.Id,
                    RecipeId = dbRecipe.Id, 
                    Quantity = x.Quantity ?? "",
                    Calories = x.Calories ?? 0
                }).ToList();
        }

        dbRecipe.Name = recipe.Name ?? dbRecipe.Name;
        dbRecipe.Description = recipe.Description ?? dbRecipe.Description;
        dbRecipe.Image = recipe.ImageUrl ?? dbRecipe.Image;
        try
        {
            var result = await RecipeRepository.UpdateAsync(dbRecipe);
            return result.ToDTO();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return dbRecipe.ToDTO();
    }


    public async Task DeleteAsync(int id)
    {
        await RecipeRepository.DeleteAsync(id);
    }

}
