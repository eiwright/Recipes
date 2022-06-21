using Microsoft.AspNetCore.Http;

using Recipe.Service.Business.Utils;

namespace Recipe.Service.Business.Mappers;

public static class RecipeExtension
{
    public static Interfaces.DTO.Recipe ToDTO(this Domain.Models.Recipe recipe)
    {
        if (recipe == null) return null;

        string imageDataURL = string.Empty;
        if (recipe.ImageData?.Length > 0)
        {
            string imageBase64Data = Convert.ToBase64String(recipe.ImageData);
            imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
        }
        return new Interfaces.DTO.Recipe
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            ImageUrl = recipe.Image,
            FileName = recipe.ImageFileName,
            ImageDataUrl = imageDataURL,
            Ingredients = recipe.Ingredients?.Select(x => x.ToDTO()).ToList(),
            Instructions = recipe.Instructions?.Select(x => x.ToDTO()).ToList(),
        };
    }

    public static Domain.Models.Recipe? ToDomain(this Interfaces.DTO.Recipe recipe)
    {
        if (recipe == null) return null;

        return new Domain.Models.Recipe
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description ?? default!,
            Image = recipe.ImageUrl,
            Ingredients = recipe.Ingredients.Select(x => new Domain.Models.RecipeIngredients { RecipeId = recipe.Id, IngredientId = x.Id }).ToList(),
            Instructions = recipe.Instructions.Select(x => x.ToDomain()).ToList()
        };
    }

    public static Domain.Models.Recipe? ToDomain(this Interfaces.DTO.WriteRecipe recipe)
    {
        if (recipe == null) return null;

        return new Domain.Models.Recipe
        {
            Name = recipe.Name,
            Description = recipe.Description ?? default!,
            Image = recipe.ImageUrl,
            //Ingredients = recipe.Ingredients.Select(x => new Domain.Models.RecipeIngredients { IngredientId = x.Id }).ToList(),
            //Instructions = recipe.Instructions.Select(x => new Domain.Models.Instruction { Id = x.Id }).ToList()
        };
    }
}
