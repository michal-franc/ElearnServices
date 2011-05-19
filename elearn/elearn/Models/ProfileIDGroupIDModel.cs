namespace elearn.Models
{
    public class ProfileIDGroupIDModel
    {
        public int ProfileId;
        public int GroupId;

        public ProfileIDGroupIDModel(int profileId, int groupId)
        {
            ProfileId = profileId;
            GroupId = groupId;
        }
    }
}