using FlavoursomeWeb.Data;
using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace FlavoursomeWeb.Repository
{
    public class StepsRepository : IStepsRepository
    {
        private readonly ApplicationDbContext _context;
        public StepsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Step>> GetStepsByRecipe(int recipeID)
        {
            return await _context.Steps
                         .Where(s => s.RecipeID == recipeID)
                         .ToListAsync();
        }
        public async Task<bool> AddAsync(Step step)
        {
            _context.Add(step);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Step step)
        {
            _context.Remove(step);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAsync(Step step)
        {
            _context.Update(step);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
