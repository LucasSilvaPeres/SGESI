using System.Security.Claims;
using System.Security.Principal;

namespace Interface.ExtensionMethods
{
    public static class IdentityExtensions
    {
        public static string GetUserType(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserType");
            return claim?.Value;
        }

        public static string GetUserId(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
            return claim?.Value;
        }

        public static string GetUserName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Name");
            return claim?.Value;
        }

        public static string GetShortUserName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Name");
            return claim?.Value.Length > 20 ? claim?.Value.Substring(0, 20) : claim?.Value;
        }
    }
}
