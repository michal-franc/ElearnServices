using System;
using System.Web;
using elearn.ProfileService;
namespace elearn.Session
{
    public  class SessionStateService
    {
        private IProfileService _profileService;

        private  NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SessionStateService(IProfileService profileService)
        {
            _profileService = profileService;
        }

        public  void AddUserDataToSession(CurrentProfileSession profileSession)
        {
            try
            {
                HttpContext.Current.Session.Add("CurrentUser", profileSession);
            }
            catch (Exception ex)
            {
                Logger.Error("Error - Adding Current user data to session state \r\n{0} \r\n{1}",ex.Message,ex.StackTrace);
            }
        }

        //todotest
        public  CurrentProfileSession GetCurrentUserDataFromSession()
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    var obj = HttpContext.Current.Session["CurrentUser"];
                    if (obj != null)
                    {
                        Logger.Debug("retrieving Current user data from session state ");
                        var currentUserSession = obj as CurrentProfileSession;
                        if (currentUserSession != null)
                            return currentUserSession;
                        else
                            return null;
                    }
                    else
                    {
                        Logger.Debug("Adding Current user data to session state ");
                        var profile = _profileService.GetByName(HttpContext.Current.User.Identity.Name);
                        var currentProfileSession = new CurrentProfileSession(profile.Role, profile.ID);

                        AddUserDataToSession(currentProfileSession);

                        return currentProfileSession;
                    }
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Error - Retrieving Current user data from session state \r\n{0} \r\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
        }
    }
}