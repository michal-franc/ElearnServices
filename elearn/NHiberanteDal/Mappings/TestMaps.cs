using NHiberanteDal.Models;
using FluentNHibernate.Mapping;

namespace NHiberanteDal.Mappings
{
    public class TestModelMap : ClassMap<TestModel>
    {
        public TestModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            Map(x => x.EditDate);

            //One
            References(x => x.Author).Not.Nullable().Not.LazyLoad();
            References(x => x.TestType).Not.Nullable().Not.LazyLoad();

            //Many
            HasMany(x => x.Questions).Cascade.All().Not.LazyLoad().KeyColumns.Add("TestId");
        }
    }

    public class TestTypeModelMap : ClassMap<TestTypeModel>
    {

        public TestTypeModelMap()
        {
            Id(x => x.ID);
            Map(x => x.TypeName).Not.Nullable();
        }
    }

    public class TestQuestionModelMap : ClassMap<TestQuestionModel>
    {

        public TestQuestionModelMap()
        {
            Id(x => x.ID);
            Map(x => x.QuestionText).Length(4001).Not.Nullable();
            Map(x => x.QuestionLabel).Not.Nullable();

            //Many
            HasMany(x => x.Answers).Cascade.AllDeleteOrphan().Not.LazyLoad().KeyColumns.Add("TestQuestionId");
        }
    }

    public class TestQuestionAnswerMap : ClassMap<TestQuestionAnswer>
    {

        public TestQuestionAnswerMap()
        {
            Id(x => x.ID);
            Map(x => x.Correct).Not.Nullable();
            Map(x => x.Text).Not.Nullable();
            Map(x => x.NumberSelected);

        }
    }

    public class FinishedTestMap : ClassMap<FinishedTestModel>
    {
        public FinishedTestMap()
        {
            Id(x => x.ID);
            Map(x=>x.DateFinished).Not.Nullable();
            Map(x => x.Mark).Not.Nullable();
            References(x=>x.Test).Not.Nullable().Not.LazyLoad();
        }
    }

}
