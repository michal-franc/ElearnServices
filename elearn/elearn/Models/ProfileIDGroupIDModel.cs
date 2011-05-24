namespace elearn.Models
{
    public class JoinGroupModel
    {
        public int ProfileId;
        public int GroupId;
        public int CourseId;
        public bool IsPasswordProtected;

        public JoinGroupModel(int profileId, int groupId,int courseId, bool isPasswordProtected)
        {
            ProfileId = profileId;
            GroupId = groupId;
            CourseId = courseId;
            IsPasswordProtected = isPasswordProtected;
        }
    }

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