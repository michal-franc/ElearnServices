using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using NHiberanteDal.Models;

namespace Models
{
    #region Models

    public class CourseModel
    {
        public virtual int ID { get;private set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string Logo { get; set; }


        //One
        public virtual CourseTypeModel CourseType { get; set; }
        public virtual GroupModel Group { get; set; }
        public virtual ForumModel Forum { get; set; }

        //Many
        public virtual List<SurveyModel> Surveys { get; set; }
        public virtual List<TestModel> Tests { get; set; }
        public virtual List<ContentModel> Contents { get; set; }
    }

    public class CourseTypeModel
    {
        public virtual int ID { get; private set; }
        public virtual string TypeName { get; set; }
    }


    #endregion
}