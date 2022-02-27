using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Organic_Shop.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetAccountID(this IIdentity identiy)
        {
            var claim = ((ClaimsIdentity)identiy).FindFirst("AccountId");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserID(this IIdentity identiy)
        {
            var claim = ((ClaimsIdentity)identiy).FindFirst("AccountId");
            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
