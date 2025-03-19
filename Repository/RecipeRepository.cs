using FlavoursomeWeb.Data;
using FlavoursomeWeb.Data.Enums;
using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FlavoursomeWeb.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _context;
        public RecipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Recipe>> GetAllRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }
        public async Task<Recipe> GetByIdUserChangesAsync(int id, string userID)
        {
            return await _context.Recipes
                                   .Include(r => r.Ingredients)
                                   .Include(r => r.Steps)
                                   .Where(r => r.ID == id && r.UserId == userID)
                                   .FirstOrDefaultAsync();
        }
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                                  .Include(r => r.Ingredients)
                                  .Include(r => r.Steps)
                                  .FirstOrDefaultAsync(i => i.ID == id);
        }
        public async Task<IEnumerable<Recipe>> GetRecipesByType(string dishType)
        {
            return await _context.Recipes
                                  .Where(r => r.RecipeType == Enum.Parse<DishType>(dishType))
                                  .ToListAsync();

        }
        public async Task<bool> AddAsync(Recipe recipe)
        {
            _context.Add(recipe);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Recipe recipe)
        {
            _context.Remove(recipe);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAsync(Recipe recipe)
        {
            _context.Update(recipe);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
