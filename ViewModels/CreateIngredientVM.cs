using FlavoursomeWeb.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FlavoursomeWeb.ViewModels
{
    public class CreateIngredientVM
    {
        [Required(ErrorMessage = "Ingredient name is required")]
        public string Name { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public double? Quantity { get; set; }
        public QuantityType? QuantityType { get; set; }
    }
}
