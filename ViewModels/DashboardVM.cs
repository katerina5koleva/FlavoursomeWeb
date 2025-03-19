using FlavoursomeWeb.Models;

namespace FlavoursomeWeb.ViewModels
{
    public class DashboardVM
    {
        public List<Recipe> UserRecipes { get; set; }
        public List<RecipeVM> FavoriteUserRecipes { get; set; }
    }
}
