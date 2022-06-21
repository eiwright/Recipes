using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Service.Business.Interfaces.DTO;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Quantity { get; set; }
    public int? Calories { get; set; }
}
