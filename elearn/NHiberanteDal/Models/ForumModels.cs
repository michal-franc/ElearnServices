using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    #region Models
    public class ForumModel
    {
        public int ID;
        //public int IDCourse; // Course
        //public int IDGroup; // Group
        public string Name;
        public string Author;

        //Todo: Uprawnienia Forum
    }

    public class TopicModel
    {
        public int ID;
        //public int IDForum; // Forum
        public string Text;
    }

    public class PostModel
    {
        public int ID;
        //public int IDTopic;
        public string Text;
    }
    #endregion
}