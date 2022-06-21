using Microsoft.EntityFrameworkCore;

using Recipe.Service.Domain.Models;
using Recipe.Service.Domain.Repositories;

namespace Recipe.Service.Data.Repository;

public class InstructionRepository : RepositoryBase<Domain.Models.Instruction, int>, IInstructionRepository
{
    public InstructionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    protected override IQueryable<Instruction> GetQueryableById(int id) =>
        DataContext.Instructions.Where(x => x.Id == id);


    protected override IQueryable<Instruction> GetQueryableByIds(IEnumerable<int> ids) =>
        DataContext.Instructions.Where(x => ids.Contains(x.Id));

    public async Task<IList<Instruction>> GetByRecipeIdAsync(int recipeId) => 
        await DataContext.Instructions.Where(x => x.RecipeId == recipeId).ToListAsync();

}
