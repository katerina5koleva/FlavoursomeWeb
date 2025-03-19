using FlavoursomeWeb.Data;
using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FlavoursomeWeb.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Favorite>> GetFavoritesByUser(string userID)
        {
            return await _context.Favorites
                                 .Include(f => f.Recipe)
                                 .Where(f => f.UserId == userID)
                                 .ToListAsync();
        }

        public async Task<List<Recipe>> GetRecipesByUserId(string userID)
        {
            return await _context.Recipes
                                 .Include(r => r.Ingredients)
                                 .Include(r => r.Steps)
                                 .Where(r => r.UserId == userID)
                                 .ToListAsync();
        }
    }
}
