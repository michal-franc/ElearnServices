using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using elearn.Session;

namespace elearn.ActionFilters
{
    public class LayoutChangerAttribute : ActionFilterAttribute
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            var result = filterContext.Result as ViewResult;
            var profileData = SessionStateService.SessionState.GetCurrentUserDataFromSession();
            if (profileData == null)
            {
                if (result != null && !filterContext.ActionDescriptor.ActionName.Contains("LogOn"))
                {
                    RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                    redirectTargetDictionary.Add("action", "LogOn");
                    redirectTargetDictionary.Add("controller", "Account");
                    filterContext.Result = new RedirectToRouteResult(redirectTargetDictionary);
                }
            }
            else if (result != null)
            {
                result.MasterName = GetMaster(profileData);
            }
        }

        private string GetMaster(CurrentProfileSession profileData)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Logger.Debug("Layout - Setting Layout to Anonymous User");
                return "_AnonymousUserLayout";
            }
            else if (profileData.Role == "admin")
            {
                Logger.Debug("Layout - Setting Layout to admin");
                return "_AdminLayout";
            }
            else if (profileData.Role == "basicuser" || profileData.Role == "courseowner")
            {
                Logger.Debug("Layout - Setting Layout to Logged User");
                return "_LoggedUserLayout";
            }
            return String.Empty;
        }
    }
}