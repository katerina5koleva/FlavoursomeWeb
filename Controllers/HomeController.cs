using FlavoursomeWeb.Helpers;
using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using FlavoursomeWeb.Repository;
using FlavoursomeWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace FlavoursomeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpsContextAccessor;
        private readonly IFavoriteRepository _favoriteRepository;

        public HomeController(IRecipeRepository recipeRepository, ILogger<HomeController> logger, IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IFavoriteRepository favoriteRepository)
        {
            _recipeRepository = recipeRepository;
            _logger = logger;
            _dashboardRepository = dashboardRepository;
            _favoriteRepository = favoriteRepository;
            _httpsContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Recipe> allRecipes = await _recipeRepository.GetAllRecipes();
            var favoriteRecipes = await _dashboardRepository.GetFavoritesByUser(userId);

            var recipeViewModels = allRecipes.Select(recipe => new RecipeVM
            {
                Recipe = recipe,
                IsFavorited = favoriteRecipes.Any(f => f.RecipeID == recipe.ID)
            }).ToList();

            return View(recipeViewModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Toggle([FromBody] ToggleFavoriteRequest request)
        {
            var userId = _httpsContextAccessor.HttpContext.User.GetUserId();
            var favorite = await _favoriteRepository.GetFavoriteAsync(request.RecipeId, userId);

            if (favorite == null)
            {
                await _favoriteRepository.AddAsync(new Favorite { UserId = userId, RecipeID = request.RecipeId });
                return Json(new { isFavorited = true });
            }
            else
            {
                await _favoriteRepository.DeleteAsync(favorite);
                return Json(new { isFavorited = false });
            }
        }
    }
}
