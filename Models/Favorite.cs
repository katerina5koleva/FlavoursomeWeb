using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlavoursomeWeb.Models
{
    public class Favorite
    {
        [Key]
        public int ID { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public User User { get; set; }
        [ForeignKey("RecipeID")]
        public int RecipeID { get; set; }
        public Recipe Recipe { get; set; }
    }
}
