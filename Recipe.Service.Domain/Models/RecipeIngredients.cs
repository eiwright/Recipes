using Recipe.Service.Domain.Entities;

namespace Recipe.Service.Domain.Models;

public class RecipeIngredients : BaseEntity<int>
{
    public int RecipeId { get; set; }
    public int IngredientId { get; set; }

    /// <summary>
    /// For simplicity using a single value, so '1/2 cup'.
    /// Later split this into Measurement enum( tsp, Tbs, cup, oz, gram, etc) and a decimal Quantity
    /// </summary>
    public string Quantity { get; set; }
    public int? Calories { get; set; }

    public Recipe Recipe { get; set; }
    public Ingredient Ingredient { get; set; }
}
