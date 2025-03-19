using FlavoursomeWeb.Models;

namespace FlavoursomeWeb.Interfaces
{
    public interface IFavoriteRepository
    { 
        Task<Favorite> GetFavoriteAsync(int id, string userID);
        Task<List<int>> GetFavoritesByRecipeID(string userID);
        Task<bool> AddAsync(Favorite favorite);
        Task<bool> DeleteAsync(Favorite favorite);
        Task<bool> SaveAsync();
    }
}
