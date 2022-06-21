using Microsoft.AspNetCore.Mvc;

namespace Recipe.Service.Controllers;


public class HomeController : Controller
{
    /// <summary>
    /// Returns some default response
    /// </summary>
    /// <returns></returns>
    public IActionResult Index() => Json(new { App = "RecipeService" });
}