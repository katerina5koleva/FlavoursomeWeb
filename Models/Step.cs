using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlavoursomeWeb.Models
{
    public class Step
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("RecipeID")]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
    }
}
