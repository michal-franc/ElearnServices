using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace Models
{
    #region Models

    public class CourseModel
    {
        public int ID;
        public string Name;
        public DateTime CreationDate;
        public string Logo;


        //One
        public CourseTypeModel CourseType;
        public GroupModel Group;
        public ForumModel Forum;

        //Many
        public List<SurveyModel> Surveys;
        public List<TestModel> Tests;
        public List<ContentModel> Contents;
    }

    public class CourseTypeModel
    {
        public int ID;
        public string TypeName;
    }

    public class SurveyModel
    {
        public int ID;
        //public int IDCourse;
        //public int IDGroup;
        public string SurveyText;

        //Many
        public List<SurveyQuestionModel> Questions;

    }

    public class SurveyQuestionModel
    {
        public int ID;
        public string QuestionText;
        public int TimesSelected;
    }

    #endregion
}