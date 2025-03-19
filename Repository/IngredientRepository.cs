using FlavoursomeWeb.Data;
using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FlavoursomeWeb.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;
        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ingredient>> GetAllByRecipeId(int recipeID)
        {
            return await _context.Ingredients
                                 .Where(i => i.RecipeID == recipeID)
                                 .ToListAsync();
        }
        public async Task<bool> AddAsync(Ingredient ingredient)
        {
            _context.Add(ingredient);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Ingredient ingredient)
        {
            _context.Remove(ingredient);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAsync(Ingredient ingredient)
        {
            _context.Update(ingredient);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        
    }
}
