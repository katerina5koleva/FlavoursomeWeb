using FlavoursomeWeb.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FlavoursomeWeb.ViewModels
{
    public class EditStepVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        public int RecipeID { get; set; }
    }
}
