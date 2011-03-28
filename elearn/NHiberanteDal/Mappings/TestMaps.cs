using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using FluentNHibernate.Mapping;

namespace NHiberanteDal.Mappings
{
    public class TestModelMap : ClassMap<TestModel> 
    {

        public TestModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.CreationDate);
            Map(x => x.EditDate);


            //One
            References(x=>x.Author);
            References(x=>x.Course);
            References(x=>x.TestType);

            //Many
            HasMany(x=>x.Questions);

        }
    }

    public class TestTypeModelMap : ClassMap<TestTypeModel>
    {

        public TestTypeModelMap()
        {
            Id(x => x.ID);
            Map(x => x.TypeName);
        }
    }

    public class TestQuestionModelMap : ClassMap<TestQuestionModel>
    {

        public TestQuestionModelMap()
        {
            Id(x => x.ID);
            Map(x => x.QuestionText);

            //Many
            HasMany(x => x.Answers);
        }
    }

    public class TestQuestionAnswerMap : ClassMap<TestQuestionAnswer>
    {

        public TestQuestionAnswerMap()
        {
            Id(x => x.ID);
            Map(x => x.Correct);
            Map(x => x.Text);
            Map(x => x.NumberSelected);

        }
    }
}
