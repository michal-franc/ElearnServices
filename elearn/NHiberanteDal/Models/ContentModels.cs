using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHiberanteDal.Models
{
    public class ContentModel : IModel
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
}