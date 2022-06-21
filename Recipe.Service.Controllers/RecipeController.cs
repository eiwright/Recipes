using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Recipe.Service.Business.Interfaces.Services;
using Recipe.Service.Domain.Models.Search;
using Recipe.Service.Business.Interfaces.DTO;
using Recipe.Service.Business.Interfaces.DTO.Search;
using Recipe.Service.Business.Interfaces.Exceptions;

namespace Recipe.Service.Controllers;

[Route("api")]
[ApiController]
public class RecipeController : ControllerBase
{
    private IRecipeService RecipeService { get; set; }

    /// <summary>
    /// </summary>
    /// <param name="userService"></param>
    public RecipeController(IRecipeService recipeService)
    {
        RecipeService = recipeService;
    }

    /// <summary>
    /// Returns information about a Recipe
    /// </summary>
    /// <returns>Customer Id and Name</returns>
    [HttpGet("Recipes/GetAll")]
    [ProducesResponseType(typeof(IList<Business.Interfaces.DTO.Recipe>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<Business.Interfaces.DTO.Recipe>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var recipes = await RecipeService.GetAllRecipesAsync();
        return Ok(recipes);
    }

    /// <summary>
    /// Returns information about a Recipe
    /// </summary>
    /// <returns>Customer Id and Name</returns>
    [HttpGet("Recipes/SearchRecipes/{search}")]
    [ProducesResponseType(typeof(IList<Business.Interfaces.DTO.Recipe>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<Business.Interfaces.DTO.Recipe>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SearchRecipes(string search)
    {
        var recipes = await RecipeService.SearchRecipesAsync(search);
        return Ok(recipes);
    }

    /// <summary>
    /// Returns information about a Recipe
    /// </summary>
    /// <returns>Customer Id and Name</returns>
    [HttpPost("Recipes/GetPagedResults")]
    [ProducesResponseType(typeof(PagedSearchResult<Business.Interfaces.DTO.Recipe>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(PagedSearchResult<Business.Interfaces.DTO.Recipe>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPagedResults([FromBody] RecipeSimpleTermSearch search)
    {
        var recipes = await RecipeService.GetRecipesAsync(search);
        return Ok(recipes);
    }

    /// <summary>
    /// Get a Recipe 
    /// </summary>
    [HttpGet("Recipes/{id:int}")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRecipe(int id)
    {
        var recipe = await RecipeService.GetRecipeAsync(id);
        if (recipe == null)
            return NotFound();
        return Ok(recipe);
    }

    /// <summary>
    /// Adds a Recipe
    /// </summary>
    /// <returns></returns>
    [HttpPost("Recipes/AddRecipe")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddRecipeAsync([FromBody] WriteRecipe recipe) =>
        StatusCode(StatusCodes.Status201Created, await RecipeService.AddRecipeAsync(recipe));

    /// <summary>
    /// Adds a Recipe Image
    /// </summary>
    /// <returns></returns>
    [HttpPost("Recipes/AddImageToRecipe")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddImageToRecipe([FromForm] WriteRecipeImage recipeImage) =>
        StatusCode(StatusCodes.Status201Created, await RecipeService.AddImageToRecipeAsync(recipeImage));
    
    /// <summary>
    /// Edit a Recipe
    /// </summary>
    /// <param name="recipeId"></param>
    /// <returns></returns>
    [HttpPost("Recipes/UpdateRecipe/{recipeId}")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRecipe(int recipeId, [FromBody] Business.Interfaces.DTO.Recipe recipe)
    {
        try
        {
            var result = await RecipeService.UpdateRecipe(recipeId, recipe);
            return new ObjectResult(result);
        }
        catch(NotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }


    /// <summary>
    /// Edit a Recipe
    /// </summary>
    /// <param name="recipeId"></param>
    /// <returns></returns>
    [HttpDelete("Recipes/DeleteRecipe/{recipeId}")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Recipe), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRecipe(int recipeId)
    {
        try
        {
            await RecipeService.DeleteAsync(recipeId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e);
        }
    }
}
