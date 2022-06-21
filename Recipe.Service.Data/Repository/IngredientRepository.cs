using Microsoft.EntityFrameworkCore;

using Recipe.Service.Domain.Models;
using Recipe.Service.Domain.Repositories;

namespace Recipe.Service.Data.Repository;

public class IngredientRepository : RepositoryBase<Domain.Models.Ingredient, int>, IIngredientRepository
{
    public IngredientRepository(DataContext dataContext) : base(dataContext)
    {
    }

    protected override IQueryable<Ingredient> GetQueryableById(int id) =>
        DataContext.Ingredients.Where(x => x.Id == id);

    protected override IQueryable<Ingredient> GetQueryableByIds(IEnumerable<int> ids) =>
        DataContext.Ingredients.Where(x => ids.Contains(x.Id));

    public async Task<IList<Domain.Models.RecipeIngredients>> GetByRecipeIdAsync(int recipeId) =>
        await DataContext.RecipeIngredients.Where(x => x.RecipeId == recipeId).ToListAsync();

    public async Task<IList<Domain.Models.Ingredient>> GetAllAsync() =>
        await DataContext.Ingredients.ToListAsync();
}

