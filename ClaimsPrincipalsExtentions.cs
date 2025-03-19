using System.Security.Claims;

namespace FlavoursomeWeb
{
    public static class ClaimsPrincipalsExtentions
    {
            public static string GetUserId(this ClaimsPrincipal user)
            {
                return user.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
    }
}
