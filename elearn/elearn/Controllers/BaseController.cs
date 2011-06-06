using System;
using System.Web.Mvc;

namespace elearn.Controllers
{
    public class BaseController : Controller
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var parameters = String.Empty;

            foreach (var param in filterContext.ActionParameters)
            {
                parameters += String.Format("Param Name : {0} , Param Value : {1} ", param.Key, param.Value);
            }

            logger.Info("Executing Controller : {0} , Action : {1} , Parameters : {2}", filterContext.Controller, actionName, parameters);
            base.OnActionExecuting(filterContext);
        }
    }
}