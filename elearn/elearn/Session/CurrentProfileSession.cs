namespace elearn.Session
{
    public class CurrentProfileSession
    {
        public string Role { get; set; }
        public int ProfileId { get; set; }

        public CurrentProfileSession(string role , int profileId)
        {
            ProfileId = profileId;
            Role = role;
        }

    }
}