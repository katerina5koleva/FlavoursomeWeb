using FlavoursomeWeb.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlavoursomeWeb.Models
{
    public class Ingredient
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("RecipeID")]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
        
        [Required]
        public string Name { get; set; }
        public double? Quantity { get; set; }
        public QuantityType? QuantityType { get; set; }
    }
}
