using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;

namespace elearn
{
    public class AuthorizeAttributeWCF : AuthorizeAttribute
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
            if (this.Users.Length > 0 && !Enumerable.Contains<string>(this.Users.Split(','), user.Identity.Name, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
            if (this.Roles.Length > 0)
            {
                string [] roles = this.Roles.Split(','); 
                var service = new ProfileService.ProfileServiceClient();
                return service.IsUserInRoles(user.Identity.Name,roles);
            }
            return true;
        }
    }
}