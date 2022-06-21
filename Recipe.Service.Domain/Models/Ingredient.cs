using Recipe.Service.Domain.Entities;

namespace Recipe.Service.Domain.Models;

public class Ingredient : BaseEntity<int>
{
    public string Name { get; set; }

    //Could add a enum for ingredient types (meat, spice, liquid, vegetable, etc) 

    public List<RecipeIngredients> Recipes { get; set; } = new List<RecipeIngredients>();
}
