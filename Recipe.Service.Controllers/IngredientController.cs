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
public class IngredientController : ControllerBase
{
    private IIngredientService IngredientService { get; set; }

    /// <summary>
    /// </summary>
    /// <param name="userService"></param>
    public IngredientController(IIngredientService ingredientService)
    {
        IngredientService = ingredientService;
    }

    /// <summary>
    /// Returns information about a Ingredient
    /// </summary>
    /// <returns>Customer Id and Name</returns>
    [HttpGet("Ingredients/GetAll")]
    [ProducesResponseType(typeof(IList<Business.Interfaces.DTO.Ingredient>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IList<Business.Interfaces.DTO.Ingredient>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var ingredients = await IngredientService.GetAllIngredientsAsync();
        return Ok(ingredients);
    }

    /// <summary>
    /// Get a Ingredient 
    /// </summary>
    [HttpGet("Ingredients/{id:int}")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Ingredient), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Ingredient), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetIngredient(int id)
    {
        var ingredient = await IngredientService.GetIngredientAsync(id);
        if (ingredient == null)
            return NotFound();
        return Ok(ingredient);
    }

    /// <summary>
    /// Adds a Ingredient
    /// </summary>
    /// <returns></returns>
    [HttpPost("Ingredients/AddIngredient")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Ingredient), StatusCodes.Status201Created)]
    public async Task<IActionResult> AddIngredientAsync([FromBody] WriteIngredient ingredient) =>
        StatusCode(StatusCodes.Status201Created, await IngredientService.AddIngredientAsync(ingredient));


    /// <summary>
    /// Edit a Ingredient
    /// </summary>
    /// <param name="ingredientId"></param>
    /// <returns></returns>
    [HttpPost("Ingredients/UpdateIngredient/{ingredientId}")]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Ingredient), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Ingredient), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Business.Interfaces.DTO.Ingredient), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateIngredient(int ingredientId, [FromBody] Business.Interfaces.DTO.Ingredient ingredient)
    {
        try
        {
            var result = await IngredientService.UpdateIngredient(ingredientId, ingredient);
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
}
