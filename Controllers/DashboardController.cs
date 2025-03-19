using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using FlavoursomeWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlavoursomeWeb.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IHttpContextAccessor _httpsContextAccessor;
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpsContextAccessor)
        {
            _dashboardRepository = dashboardRepository;
            _httpsContextAccessor = httpsContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            string userId = _httpsContextAccessor.HttpContext.User.GetUserId();

            // Get User Recipes
            List<Recipe> userRecipes = await _dashboardRepository.GetRecipesByUserId(userId);

            // Get Favorite Recipes
            List<Favorite> favoriteUserRecipes = await _dashboardRepository.GetFavoritesByUser(userId);

            // Convert Favorites to RecipeVM
            List<RecipeVM> favoriteRecipesVM = favoriteUserRecipes
                .Select(fav => new RecipeVM
                {
                    Recipe = fav.Recipe,
                    IsFavorited = true
                }).ToList();

            // Create DashboardVM
            DashboardVM dashboardVM = new DashboardVM
            {
                UserRecipes = userRecipes,
                FavoriteUserRecipes = favoriteRecipesVM
            };

            return View(dashboardVM);
        }
    }
}
