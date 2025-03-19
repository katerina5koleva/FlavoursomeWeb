using System.ComponentModel.DataAnnotations;

namespace FlavoursomeWeb.ViewModels
{
    public class CreateStepVM
    {
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
}
