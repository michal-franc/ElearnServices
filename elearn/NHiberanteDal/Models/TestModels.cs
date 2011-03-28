using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{

    //Todo : Group which would store who completed test
    #region Models
    public class TestModel
    {
        public int ID;
        public string Name;
        //Kurs
        
        public DateTime CreationDate;
        public DateTime EditDate;

        //One
        public ProfileModel Author;
        public CourseModel Course;
        public TestTypeModel TestType;

        //Many
        public List<TestQuestionModel> Questions;


    }

    public class TestTypeModel
    {
        public int ID;
        public string TypeName;
    }

    public class TestQuestionModel
    {
        public int ID;
        public string QuestionText;
        public List<TestQuestionAnswer> Answers;
    }

    public class TestQuestionAnswer
    {
        public int ID;
        public string Text;
        public bool Correct;
        public int NumberSelected;
    }
    #endregion
}