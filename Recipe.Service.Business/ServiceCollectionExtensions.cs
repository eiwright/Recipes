using Microsoft.Extensions.DependencyInjection;
using Recipe.Service.Business.Interfaces.Services;

namespace Recipe.Service.Business.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRecipeService, RecipeService>();
        services.AddScoped<IIngredientService, IngredientService>();
        return services;
    }
}
