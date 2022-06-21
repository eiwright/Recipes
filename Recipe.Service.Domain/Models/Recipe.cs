using Recipe.Service.Domain.Entities;

namespace Recipe.Service.Domain.Models;

public class Recipe : BaseEntity<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public byte[]? ImageData { get; set; }
    public string? ImageFileName { get; set; }
    public List<RecipeIngredients> Ingredients { get; set; } = new List<RecipeIngredients>();
    public List<Instruction> Instructions { get; set; } = new List<Instruction>();

}