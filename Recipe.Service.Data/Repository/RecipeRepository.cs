using Microsoft.EntityFrameworkCore;

using Recipe.Service.Domain.Enums;
using Recipe.Service.Domain.Models.Search;
using Recipe.Service.Domain.Repositories;

namespace Recipe.Service.Data.Repository;

public class RecipeRepository : RepositoryBase<Domain.Models.Recipe, int>, IRecipeRepository
{
    public RecipeRepository(DataContext dataContext) : base(dataContext)
    {
    }

    protected override IQueryable<Domain.Models.Recipe> GetQueryableById(int id) =>
        DataContext.Recipes.Where(x => x.Id == id);

    protected override IQueryable<Domain.Models.Recipe> GetQueryableByIds(IEnumerable<int> ids) =>
        DataContext.Recipes.Where(x => ids.Contains(x.Id));

    public async Task<Domain.Models.Recipe> GetByRecipeIdAsync(int id) =>
        await GetQueryableById(id)
        .Include(x => x.Ingredients).ThenInclude(x => x.Ingredient)
        .Include(x => x.Instructions.OrderBy(i => i.Order))
        .FirstOrDefaultAsync()
        .ConfigureAwait(false);

    public async Task<IList<Domain.Models.Recipe>> GetAllAsync() => 
        await DataContext.Recipes
        .Include(x => x.Ingredients).ThenInclude(x => x.Ingredient)
        .Include(x => x.Instructions.OrderBy(i => i.Order))
        .ToListAsync();

    public async Task<IList<Domain.Models.Recipe>> SearchRecipesAsync(string search)
    {
        var query = DataContext.Recipes
            .Include(x => x.Ingredients).ThenInclude(x => x.Ingredient)
            .Include(x => x.Instructions.OrderBy(i => i.Order))
            .AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(r =>
                r.Name.Contains(search) ||
                r.Description.Contains(search)
                )
                .Where(r => r.Instructions.Select(i => i.Description.Contains(search)).Any()
                    || r.Ingredients.Select(i => i.Ingredient.Name.Contains(search)).Any())
                .AsQueryable();
        }
        return await query.ToListAsync();
    }

    public async Task<PagedSearchResult<Domain.Models.Recipe>> GetPagedResultsAsync(PagedSearch search)
    {
        var query = DataContext.Recipes.AsQueryable();
        if (!string.IsNullOrEmpty(search.SearchString))
        {
            query = query.Where(r => r.Name.Contains(search.SearchString));
        }

        //get SortField or use Name
        var valid = Enum.TryParse<RecipeSortTerms>(search.SortField ?? nameof(RecipeSortTerms.Name), out var sortTerm);
        if (!valid)
            throw new KeyNotFoundException("SortField");

        query = sortTerm switch
        {
            RecipeSortTerms.Name => CreateSortQuery(query, x => x.Name, search.SortAsc),
            RecipeSortTerms.Description => CreateSortQuery(query, x => x.Description, search.SortAsc),
            RecipeSortTerms.Ingredient => CreateSortQuery(query, x => x.Ingredients, search.SortAsc),
            _ => throw new MissingFieldException("sortTerm")
        };
        var totalCount = await query.LongCountAsync();
        var results = await query.Include(x => x.Ingredients).ToListAsync().ConfigureAwait(false);

        return new PagedSearchResult<Domain.Models.Recipe>
        {
            PageNumber = search.PageNumber,
            PageSize = search.PageSize,
            Records = results.Skip(search.PageSize * (search.PageNumber - 1)).Take(search.PageSize).ToList(),
            TotalRecords = totalCount
        };
    }

    public override async Task<Domain.Models.Recipe> UpdateAsync(Domain.Models.Recipe model)
    {
        DataContext.Update(model);
        await DataContext.SaveChangesAsync();
        return model;
    }

}