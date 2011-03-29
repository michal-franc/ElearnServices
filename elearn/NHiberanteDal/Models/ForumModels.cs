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
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }

        public virtual List<TopicModel> Topics { get; set; }
         
        //Todo: Uprawnienia Forum
    }

    public class TopicModel
    {
        public virtual int ID { get;private set; }
        //public int IDForum; // Forum
        public virtual string Text { get; set; }

        public virtual List<PostModel> Posts { get; set; }

    }

    public class PostModel
    {
        public virtual int ID { get;private set; }
        //public int IDTopic;
        public virtual string Text { get; set; }
    }
    #endregion
}