using Microsoft.AspNetCore.Http;

using Recipe.Service.Domain.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipe.Service.Business.Interfaces.DTO;

public class WriteRecipe
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(250)]
    public string Description { get; set; }
    public IEnumerable<Ingredient> Ingredients { get; set; }
    public IEnumerable<Instruction> Instructions { get; set; }

    public string? ImageUrl { get; set; }
}
