using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    #region Models
    public class ContentModel
    {
        public virtual int ID { get;private set; }
        public virtual string ContentUrl { get; set; }
        public virtual string Name { get; set; }
        public virtual string Text { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime EditDate { get; set; }
        public virtual int DownloadNumber { get; set; }

        //Reference
        public virtual ContentTypeModel Type { get; set; }

    }

    public class ContentTypeModel
    {
        public virtual int ID { get;private set; }
        public virtual string TypeName { get; set; }
    }

    public class TagModel
    {
    } 
    #endregion
}