using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace NHiberanteDal.Models
{
    #region Models
    public class ForumModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string Name { get; set; }
        public virtual string Author { get; set; }

        public virtual IList<TopicModel> Topics { get; set; }
         
        //Todo: Uprawnienia Forum
    }

    public class TopicModel : IModel
    {
        public virtual int ID { get;private set; }
        //public int IDForum; // Forum
        public virtual string Text { get; set; }

        public virtual IList<PostModel> Posts { get; set; }

    }

    public class PostModel : IModel
    {
        public virtual int ID { get;private set; }
        //public int IDTopic;
        public virtual string Text { get; set; }
    }
    #endregion
}