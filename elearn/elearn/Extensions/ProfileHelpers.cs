using System.Web;
using System.Web.Mvc;
using elearn.Session;

namespace elearn.Extensions
{
    public static class ProfileHelpers
    {
        public static HtmlString GetUserName(this HtmlHelper helper)
        {
            var userData = SessionStateService.SessionState.GetCurrentUserDataFromSession();
            return new HtmlString(userData.DisplayName);
        }

        public static HtmlString GetUserRole(this HtmlHelper helper)
        {
            var userData = SessionStateService.SessionState.GetCurrentUserDataFromSession();
            return new HtmlString(userData.Role);
        }

        public static HtmlString GetUserId(this HtmlHelper helper)
        {
            var userData = SessionStateService.SessionState.GetCurrentUserDataFromSession();
            return new HtmlString(userData.ProfileId.ToString());
        }
    }
}