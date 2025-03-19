using FlavoursomeWeb.Interfaces;
using FlavoursomeWeb.Models;
using FlavoursomeWeb.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FlavoursomeWeb.Helpers;

namespace FlavoursomeWeb.Controllers
{
    [Authorize]
    public class RecipeController : Controller
    {
        private readonly IHttpContextAccessor _httpsContextAccessor;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IStepsRepository _stepsRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IPhotoService _photoService;
        private readonly IDashboardRepository _dashboardRepository;

        public RecipeController(IRecipeRepository recipeRepository, IIngredientRepository ingredientRepository, IStepsRepository stepsRepository, IFavoriteRepository favoriteRepository, IHttpContextAccessor httpsContextAccessor, IPhotoService photoService, IDashboardRepository dashboardRepository)
        {
            _recipeRepository = recipeRepository;
            _ingredientRepository = ingredientRepository;
            _stepsRepository = stepsRepository;
            _favoriteRepository = favoriteRepository;
            _httpsContextAccessor = httpsContextAccessor;
            _photoService = photoService;
            _dashboardRepository = dashboardRepository;
        }

        [AllowAnonymous]
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
        [AllowAnonymous]
        public async Task<IActionResult> Breakfast()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Recipe> breakfastRecipes = await _recipeRepository.GetRecipesByType("Breakfast");
            var favoriteRecipes = await _dashboardRepository.GetFavoritesByUser(userId);

            var recipeViewModels = breakfastRecipes.Select(recipe => new RecipeVM
            {
                Recipe = recipe,
                IsFavorited = favoriteRecipes.Any(f => f.RecipeID == recipe.ID)
            }).ToList();

            return View(recipeViewModels);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Lunch()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Recipe> lunchRecipes = await _recipeRepository.GetRecipesByType("Lunch");
            var favoriteRecipes = await _dashboardRepository.GetFavoritesByUser(userId);

            var recipeViewModels = lunchRecipes.Select(recipe => new RecipeVM
            {
                Recipe = recipe,
                IsFavorited = favoriteRecipes.Any(f => f.RecipeID == recipe.ID)
            }).ToList();

            return View(recipeViewModels);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Dinner()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Recipe> dinnerRecipes = await _recipeRepository.GetRecipesByType("Dinner");
            var favoriteRecipes = await _dashboardRepository.GetFavoritesByUser(userId);

            var recipeViewModels = dinnerRecipes.Select(recipe => new RecipeVM
            {
                Recipe = recipe,
                IsFavorited = favoriteRecipes.Any(f => f.RecipeID == recipe.ID)
            }).ToList();

            return View(recipeViewModels);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Dessert()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            IEnumerable<Recipe> dessertRecipes = await _recipeRepository.GetRecipesByType("Dessert");
            var favoriteRecipes = await _dashboardRepository.GetFavoritesByUser(userId);

            var recipeViewModels = dessertRecipes.Select(recipe => new RecipeVM
            {
                Recipe = recipe,
                IsFavorited = favoriteRecipes.Any(f => f.RecipeID == recipe.ID)
            }).ToList();

            return View(recipeViewModels);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Recipe recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            var favoriteRecipes = await _dashboardRepository.GetFavoritesByUser(userId);

            var recipeViewModel = new RecipeVM
            {
                Recipe = recipe,
                IsFavorited = favoriteRecipes.Any(f => f.RecipeID == recipe.ID)
            };

            return View(recipeViewModel);
        }

        public IActionResult Create()
        {
            CreateRecipeVM createRecipeVM = new CreateRecipeVM
            {
                VMIngredients = new List<CreateIngredientVM>
                {
                    new CreateIngredientVM(),
                    new CreateIngredientVM(),
                    new CreateIngredientVM()
                },
                VMSteps = new List<CreateStepVM>
                {   new CreateStepVM(),
                    new CreateStepVM(),
                    new CreateStepVM()
                }
            };
            return View(createRecipeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRecipeVM recipeVM)
        {
            var currentUserId = _httpsContextAccessor.HttpContext.User.GetUserId();
            recipeVM.UserID = currentUserId;
            ModelState.Remove(nameof(recipeVM.UserID));
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(recipeVM.Image);
                
                Recipe recipe = new Recipe
                {
                    UserId = currentUserId,
                    Name = recipeVM.Name,
                    Servings = recipeVM.Servings,
                    RecipeType = recipeVM.RecipeType,
                    Image = result.Url.ToString(),
                    TimeMinutes = recipeVM.TimeMinutes
                };
            
                await _recipeRepository.AddAsync(recipe);
            
                foreach (CreateIngredientVM ingredientVM in recipeVM.VMIngredients)
                {
                    Ingredient ingredient = new Ingredient
                    {
                        Name = ingredientVM.Name,
                        Quantity = ingredientVM.Quantity,
                        QuantityType = ingredientVM.QuantityType,
                        RecipeID = recipe.ID
                    };
            
                    await _ingredientRepository.AddAsync(ingredient);
                }
            
                foreach (CreateStepVM stepVM in recipeVM.VMSteps)
                {
                    Step step = new Step
                    {
                        Description = stepVM.Description,
                        RecipeID = recipe.ID
                    };
            
                    await _stepsRepository.AddAsync(step);
                }
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(recipeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            string userId = _httpsContextAccessor.HttpContext.User.GetUserId();
            Recipe recipe = await _recipeRepository.GetByIdUserChangesAsync(id, userId);
            if (recipe == null)
            {
                ViewData["ErrorMessage"] = "A problem occurred while proceeding with your request. Access denied.";
                return View("Error");
            }
            EditRecipeVM editRecipeVM = new EditRecipeVM
            {
                ID = recipe.ID,
                UserId = recipe.UserId,
                Name = recipe.Name,
                CookingTimeHours = recipe.TimeMinutes / 60,
                CookingTimeMinutes = recipe.TimeMinutes % 60,
                TimeMinutes = recipe.TimeMinutes,
                Servings = recipe.Servings,
                RecipeType = recipe.RecipeType,
                URL = recipe.Image,
                EditIngredients = recipe.Ingredients
                                .Select(ei => new EditIngredientVM
                {
                    ID = ei.ID,
                    RecipeID = ei.RecipeID,
                    Name = ei.Name,
                    Quantity = ei.Quantity,
                    QuantityType = ei.QuantityType
                })
                .ToList(),
                EditSteps = recipe.Steps
                            .Select(es => new EditStepVM
                {
                    ID = es.ID,
                    Description = es.Description,
                    RecipeID = es.RecipeID
                })
                .ToList()
            };
            return View(editRecipeVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRecipeVM editRecipeVM)
        {
            string userId = _httpsContextAccessor.HttpContext.User.GetUserId();
            editRecipeVM.UserId = userId;
            
            if (!ModelState.IsValid)
            {
               ModelState.AddModelError("", "Failed to edit recipe.");
               return View("Edit", editRecipeVM);
            }

            Recipe recipe = await _recipeRepository.GetByIdUserChangesAsync(editRecipeVM.ID, userId);
            if (recipe == null)
            {
                ViewData["ErrorMessage"] = "A problem occurred while processing your request. Access denied.";
                return View("Error");
            }

            try
            {
                await _photoService.DeletePhotoAsync(recipe.Image);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Could not delete photo.");
                return View(editRecipeVM);
            }

            var photoResult = await _photoService.AddPhotoAsync(editRecipeVM.Image);

            recipe.Name = editRecipeVM.Name;
            recipe.TimeMinutes = editRecipeVM.TimeMinutes;
            recipe.Servings = editRecipeVM.Servings;
            recipe.RecipeType = editRecipeVM.RecipeType;
            recipe.Image = photoResult.Url.ToString();

            foreach (Ingredient ingredient in recipe.Ingredients.ToList())
            {
                var updatedIngredient = editRecipeVM.EditIngredients
                    .FirstOrDefault(ei => ei.ID == ingredient.ID);

                if (updatedIngredient != null)
                {
                    ingredient.Name = updatedIngredient.Name;
                    ingredient.Quantity = updatedIngredient.Quantity;
                    ingredient.QuantityType = updatedIngredient.QuantityType;
                }
                else
                {
                    await _ingredientRepository.DeleteAsync(ingredient);
                }
            }

            foreach (var newIngredient in editRecipeVM.EditIngredients
                .Where(ei => ei.ID == 0))
            {
                var ingredient = new Ingredient
                {
                    Name = newIngredient.Name,
                    Quantity = newIngredient.Quantity,
                    QuantityType = newIngredient.QuantityType,
                    RecipeID = recipe.ID
                };
                await _ingredientRepository.AddAsync(ingredient);
            }

            foreach (Step step in recipe.Steps.ToList())
            {
                var updatedStep = editRecipeVM.EditSteps
                    .FirstOrDefault(es => es.ID == step.ID);

                if (updatedStep != null)
                {
                    step.Description = updatedStep.Description;
                }
                else
                {
                    await _stepsRepository.DeleteAsync(step);
                }
            }
            foreach (var newStep in editRecipeVM.EditSteps
                .Where(es => es.ID == 0))
            {
                Step step = new Step
                {
                    Description = newStep.Description,
                    RecipeID = recipe.ID
                };
                await _stepsRepository.AddAsync(step);
            }
            await _recipeRepository.UpdateAsync(recipe);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            Recipe recipeDetails = await _recipeRepository.GetRecipeByIdAsync(id);
            if (recipeDetails == null)
            {
                return View("Error");
            }
            return View(recipeDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            Recipe recipeDetails = await _recipeRepository.GetRecipeByIdAsync(id);
            if (recipeDetails == null)
            {
                return View("Error");
            }
            await _recipeRepository.DeleteAsync(recipeDetails);
            return RedirectToAction("Index");
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
