using FlavoursomeWeb.Models;

namespace FlavoursomeWeb.Interfaces
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Recipe>> GetAllRecipes();
        Task<Recipe> GetRecipeByIdAsync(int id);
        Task<Recipe> GetByIdUserChangesAsync(int id, string userID);
        Task<IEnumerable<Recipe>> GetRecipesByType(string dishType);
        Task<bool> AddAsync(Recipe recipe);
        Task<bool> UpdateAsync(Recipe recipe);
        Task<bool> DeleteAsync(Recipe recipe);
        Task<bool> SaveAsync();
    }
}
