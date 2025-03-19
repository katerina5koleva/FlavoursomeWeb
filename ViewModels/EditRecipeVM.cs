using FlavoursomeWeb.Data.Enums;
using FlavoursomeWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace FlavoursomeWeb.ViewModels
{
    public class EditRecipeVM
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(0, 23, ErrorMessage = "Hours should be between 0 and 23.")]
        public int? CookingTimeHours { get; set; } = 0; // Default to 0

        [Range(0, 59, ErrorMessage = "Minutes should be less than 60.")]
        public int? CookingTimeMinutes { get; set; } = 0; // Default to 0

        [Range(1, int.MaxValue, ErrorMessage = "Total cooking time must be greater than zero.")]
        public int TimeMinutes
        {
            get => (CookingTimeHours ?? 0) * 60 + (CookingTimeMinutes ?? 0);
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Cooking time must be greater than zero.");
                CookingTimeHours = value / 60;
                CookingTimeMinutes = value % 60;
            }
        }

        [Range(1, int.MaxValue, ErrorMessage = "Servings must be at least 1.")]
        public int Servings { get; set; } = 1; // Default value
        [Required]
        public DishType RecipeType { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public virtual List<EditIngredientVM> EditIngredients { get; set; }
        public virtual List<EditStepVM> EditSteps { get; set; }
    }
}
