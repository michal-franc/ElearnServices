using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    #region Models
    public class ForumModel
    {
        public virtual int ID { get; private set; }
        //public int IDCourse; // Course
        //public int IDGroup; // Group
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }
         
        //Todo: Uprawnienia Forum
    }

    public class TopicModel
    {
        public virtual int ID { get;private set; }
        //public int IDForum; // Forum
        public virtual string Text { get; set; }
    }

    public class PostModel
    {
        public virtual int ID { get;private set; }
        //public int IDTopic;
        public virtual string Text { get; set; }
    }
    #endregion
}