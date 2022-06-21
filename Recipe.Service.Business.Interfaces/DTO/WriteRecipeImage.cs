using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations;

namespace Recipe.Service.Business.Interfaces.DTO;

public class WriteRecipeImage
{
    [Required]
    public int Id { get; set; }

    [Required]
    public IFormFile? ImageData { get; set; }
}
