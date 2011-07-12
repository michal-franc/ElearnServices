using NHiberanteDal.DTO;

namespace elearn.Session
{
    public class CurrentProfileSession
    {
        public string Role { get; set; }
        public int ProfileId { get; set; }
        public string LoginName { get; set; }
        public string DisplayName { get; set; }

        public CurrentProfileSession(ProfileModelDto profileData)
        {
            ProfileId = profileData.ID;
            Role = profileData.Role;
            LoginName = profileData.LoginName;
            DisplayName = profileData.DisplayName;
        }

    }
}