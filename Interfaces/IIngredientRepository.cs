using FlavoursomeWeb.Models;

namespace FlavoursomeWeb.Interfaces
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllByRecipeId(int recipeID);
        Task<bool> AddAsync(Ingredient ingredient);
        Task<bool> UpdateAsync(Ingredient ingredient);
        Task<bool> DeleteAsync(Ingredient ingredient);
        Task<bool> SaveAsync();

    }
}
