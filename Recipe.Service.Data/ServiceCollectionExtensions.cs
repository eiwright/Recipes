using Microsoft.Extensions.DependencyInjection;

using Recipe.Service.Data.Repository;
using Recipe.Service.Domain.Repositories;

namespace Recipe.Service.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IInstructionRepository, InstructionRepository>();
        return services;
    }
}
