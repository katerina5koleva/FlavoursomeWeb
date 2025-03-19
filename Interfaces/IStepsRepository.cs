using FlavoursomeWeb.Models;

namespace FlavoursomeWeb.Interfaces
{
    public interface IStepsRepository
    {
        Task<IEnumerable<Step>> GetStepsByRecipe(int recipeID);
        Task<bool> AddAsync(Step step);
        Task<bool> UpdateAsync(Step step);
        Task<bool> DeleteAsync(Step step);
        Task<bool> SaveAsync();
    }
}
