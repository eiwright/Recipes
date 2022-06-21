using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipe.Service.Business.Interfaces.DTO;

public class WriteIngredient
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
}
