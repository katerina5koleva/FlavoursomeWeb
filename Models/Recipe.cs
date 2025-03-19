using FlavoursomeWeb.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlavoursomeWeb.Models
{
    public class Recipe
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required]
        public int TimeMinutes { get; set; }
        [Required]
        public int Servings { get; set; }
        [Required]
        public DishType RecipeType { get; set; }
        [Required]
        public string Image { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public virtual List<Favorite> FavoritedByUsers { get; set; } = new List<Favorite>();
        public virtual List<Step> Steps { get; set; } = new List<Step>();
    }
}
