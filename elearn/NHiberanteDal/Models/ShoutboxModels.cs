using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    #region Models

    public class ShoutboxModel
    {
        public virtual int ID { get;private set; }
        public virtual CourseModel CourseOwner { get; set; }
       
        //Many
        public virtual List<ShoutBoxMessageModel> Messages { get; set; }
    }

    public class ShoutBoxMessageModel
    {
        public virtual int ID { get; private set; }
        public virtual string Author { get; set; }
        public virtual DateTime TimePosted { get; set; }
        public virtual string Message { get; set; }
    }

    #endregion
}