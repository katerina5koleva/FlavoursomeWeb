using FlavoursomeWeb.Data;
using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FlavoursomeWeb.Repository
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;
        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Favorite> GetFavoriteAsync(int recipeID, string userID)
        {
            return await _context.Favorites
                                 .Where(f => f.RecipeID == recipeID
                                 && f.UserId == userID )
                                 .FirstOrDefaultAsync();
        }

        public async Task<List<int>> GetFavoritesByRecipeID(string userID)
        {
            return await _context.Favorites
                                 .Where(f => f.UserId == userID)
                                 .Select(f => f.RecipeID)
                                 .ToListAsync();
        }

        public async Task<bool> AddAsync(Favorite favorite)
        {
            _context.Add(favorite);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Favorite favorite)
        {
            _context.Remove(favorite);
            return await SaveAsync();
        }


        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
