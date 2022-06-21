using Recipe.Service.Domain.Entities;

namespace Recipe.Service.Domain.Models;

public class Instruction : BaseEntity<int>
{
    public string Description { get; set; }
    public int Order { get; set; }

    public int? RecipeId { get; set; }
    public Recipe Recipe { get; set; }

    //Could go crazy here and add lots of additional properties:  Temp, Time, etc.
    //Could also add list of Steps
    //Could add list of Ingredients used in each Instruction.
}
