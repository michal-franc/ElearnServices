using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace NHiberanteDal.Models
{
    #region Models

    public class CourseModel : IModel
    {
        public virtual int ID { get;private set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string  Logo { get; set; }
        public virtual string Description { get; set; }

        //One
        public virtual CourseTypeModel CourseType { get; set; }
        public virtual GroupModel Group { get; set; }
        public virtual ForumModel Forum { get; set; }
        public virtual ShoutboxModel ShoutBox { get; set; }

        //Many
        public virtual IList<SurveyModel> Surveys { get; set; }
        public virtual IList<TestModel> Tests { get; set; }
        public virtual IList<ContentModel> Contents { get; set; }
    }
    #endregion
}