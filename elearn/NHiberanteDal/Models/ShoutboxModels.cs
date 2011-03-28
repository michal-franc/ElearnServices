using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    #region Models

    public class ShoutboxModel
    {
        public int ID;
        public CourseModel CourseOwner;
       
        //Many
        public List<ShoutBoxMessageModel> Messages;
    }

    public class ShoutBoxMessageModel
    {
        public int ID;
        public string Author;
        public DateTime TimePosted;
        public string Message;
    }

    #endregion
}