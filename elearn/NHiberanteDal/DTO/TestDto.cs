using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    public class TestDto : DtoBaseClass<TestDto,TestModel>
    {
        public int ID { get; private set; }
        public string Name { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime EditDate { get; set; }

        //One
        //public ProfileModelDto Author { get; set; }
        //public TestTypeModel TestType { get; set; }

        //Many
        //public virtual IList<TestQuestionModel> Questions { get; set; }
    }
}
