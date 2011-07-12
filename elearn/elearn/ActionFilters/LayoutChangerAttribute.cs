using System;
using System.Web;
using System.Web.Mvc;
using elearn.Session;
using elearn.ProfileService;

namespace elearn.ActionFilters
{
    public class LayoutChangerAttribute : ActionFilterAttribute
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            if (result != null)
            {
                result.MasterName = GetMaster(SessionStateService.SessionState.GetCurrentUserDataFromSession());
            }
        }

        private string GetMaster(CurrentProfileSession profileData)
        {

            if(!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Logger.Debug("Layout - Setting Layout to Anonymous User");
                return "_AnonymousUserLayout";
            }

            if (profileData.Role == "basicuser" || profileData.Role == "admin")
            {
                Logger.Debug("Layout - Setting Layout to Logged User");
                return "_LoggedUserLayout";
            }

            return String.Empty;
        }
    }
}