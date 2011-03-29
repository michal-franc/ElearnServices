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
        public virtual int ID { get;private set; }
        public virtual string Name { get; set; }
        //Kurs

        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime EditDate { get; set; }
         
        //One
        public virtual ProfileModel Author { get; set; }
        public virtual CourseModel Course { get; set; }
        public virtual TestTypeModel TestType { get; set; }

        //Many
        public virtual List<TestQuestionModel> Questions { get; set; }


    }

    public class TestTypeModel
    {
        public virtual int ID { get;private set; }
        public virtual string TypeName { get; set; }
    }

    public class TestQuestionModel
    {
        public virtual int ID { get; private set; }
        public virtual string QuestionText { get; set; }
        public virtual List<TestQuestionAnswer> Answers { get; set; }
    }

    public class TestQuestionAnswer
    {
        public virtual int ID { get;private set; }
        public virtual string Text { get; set; }
        public virtual bool Correct { get; set; }
        public virtual int NumberSelected { get; set; }
    }
    #endregion
}