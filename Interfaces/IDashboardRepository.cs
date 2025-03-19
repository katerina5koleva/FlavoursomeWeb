using FlavoursomeWeb.Models;

namespace FlavoursomeWeb.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Recipe>> GetRecipesByUserId(string userID);
        Task<List<Favorite>> GetFavoritesByUser(string userID);
    }
}
