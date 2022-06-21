using Recipe.Service.Domain.Interfaces;

namespace Recipe.Service.Domain.Repositories;

public interface IInstructionRepository : IRepositoryBase<Models.Instruction, int>
{
    Task<IList<Domain.Models.Instruction>> GetByRecipeIdAsync(int recipeId);
}