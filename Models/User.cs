using Microsoft.AspNetCore.Identity;

namespace FlavoursomeWeb.Models
{
    public class User : IdentityUser
    {
        public List<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
