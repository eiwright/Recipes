using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service.Business.Mappers;

public static class IngredientExtension
{
    public static Interfaces.DTO.Ingredient ToDTO(this Domain.Models.Ingredient ingredient)
    {
        if (ingredient == null) return null;

        return new Interfaces.DTO.Ingredient
        {
            Id = ingredient.Id,
            Name = ingredient.Name
        };
    }

    public static Interfaces.DTO.Ingredient ToDTO(this Domain.Models.RecipeIngredients ingredient)
    {
        if (ingredient == null) return null;

        return new Interfaces.DTO.Ingredient
        {
            Id = ingredient?.Ingredient?.Id ?? 0,
            Name = ingredient?.Ingredient?.Name ?? string.Empty,
            Quantity = ingredient?.Quantity,
            Calories = ingredient.Calories
        };
    }


    public static Domain.Models.Ingredient ToDomain(this Interfaces.DTO.Ingredient ingredient)
    {
        if (ingredient == null) return null;

        return new Domain.Models.Ingredient
        {
            Id = ingredient.Id,
            Name = ingredient.Name
        };
    }

    public static Domain.Models.Ingredient? ToDomain(this Interfaces.DTO.WriteIngredient ingredient)
    {
        if (ingredient == null) return null;

        return new Domain.Models.Ingredient
        {
            Name = ingredient.Name
        };
    }
}
