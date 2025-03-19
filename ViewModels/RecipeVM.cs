using FlavoursomeWeb.Models;

namespace FlavoursomeWeb.ViewModels
{
    public class RecipeVM
    {
        public Recipe Recipe { get; set; }
        public bool IsFavorited { get; set; }
    }
}
