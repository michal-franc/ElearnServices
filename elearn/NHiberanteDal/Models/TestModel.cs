using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHiberanteDal.Models
{
    public class TestModel : IModel
    {
        public virtual int ID { get;set; }
        public virtual string Name { get; set; }
        //Kurs

        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime EditDate { get; set; }
         
        //One
        public virtual ProfileModel Author { get; set; }
        public virtual TestTypeModel TestType { get; set; }

        //Many
        public virtual IList<TestQuestionModel> Questions { get; set; }

        public TestModel()
        {
            Questions = new List<TestQuestionModel>();
        }

    }
}