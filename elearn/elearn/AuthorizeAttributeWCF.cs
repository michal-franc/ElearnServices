using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace elearn
{
    public class AuthorizeAttributeWcf : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            IPrincipal user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }
            if (Users.Length > 0 && !Users.Split(',').Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
            if (Roles.Length > 0)
            {
                string [] roles = Roles.Split(','); 
                var service = new ProfileService.ProfileServiceClient();
                return service.IsUserInRoles(user.Identity.Name,roles);
            }
            return true;
        }
    }
}