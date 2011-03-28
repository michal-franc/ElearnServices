using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    #region Models
    public class ContentModel
    {
        public int ID;
        public string ContentUrl;
        public string Name;
        public string Text;
        public DateTime CreationDate;
        public DateTime EditDate;
        public int DownloadNumber;


        public int IDCourse;

        //Reference
        public ContentTypeModel Type;

    }

    public class ContentTypeModel
    {
        public int ID;
        public string TypeName;
    }

    public class TagModel
    {
    } 
    #endregion
}