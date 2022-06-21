using Microsoft.AspNetCore.Http;

namespace Recipe.Service.Business.Interfaces.DTO;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string? ImageDataUrl { get; set; }
    public string? FileName { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
    public List<Instruction> Instructions { get; set; } = new List<Instruction>();
}
