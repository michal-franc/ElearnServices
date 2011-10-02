using System;
using System.Web;
using elearn.ProfileService;
namespace elearn.Session
{

    public static class SessionStateService
    {
        private static SessionState _sessionState;

        public static SessionState SessionState
        {
          get
          {
              if (_sessionState == null)
              {
                  _sessionState = new SessionState(new ProfileServiceClient());
              }
              return _sessionState;
          }
        }
    }


    public  class SessionState
    {
        private readonly IProfileService _profileService;

        private  NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public SessionState(IProfileService profileService)
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

        public void DeleteCurrentUserSession()
        {
            Logger.Debug("Delete Current user data from session state ");
            try
            {
                var obj = HttpContext.Current.Session["CurrentUser"];
                if (obj != null)
                {
                    HttpContext.Current.Session.Remove("CurrentUser");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error - Deleting Current user data from session state \r\n{0} \r\n{1}", ex.Message, ex.StackTrace);
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
                        var profile = _profileService.GetByNameSignature(HttpContext.Current.User.Identity.Name);
                        var currentProfileSession = new CurrentProfileSession(profile);

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