using System;
using System.Collections.Generic;

namespace NHiberanteDal.Models 
{
    #region Models

    public class CourseModel : IModel
    {
        public virtual int ID { get;set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual string  Logo { get; set; }
        public virtual string Description { get; set; }
        public virtual string ShortDescription { get; set; }
        public virtual string Password { get; set; }

        //One
        public virtual CourseTypeModel CourseType { get; set; }
        public virtual GroupModel Group { get; set; }
        public virtual ForumModel Forum { get; set; }
        public virtual ShoutboxModel ShoutBox { get; set; }

        //Many
        public virtual IList<SurveyModel> Surveys { get; set; }
        public virtual IList<TestModel> Tests { get; set; }
        public virtual IList<ContentModel> Contents { get; set; }
        public virtual IList<LearningMaterialModel> LearningMaterials { get; set; }

        public CourseModel()
        {
            Surveys = new List<SurveyModel>();
            Tests = new List<TestModel>();
            Contents = new List<ContentModel>();
            LearningMaterials = new List<LearningMaterialModel>();
        }

    }
    #endregion
}