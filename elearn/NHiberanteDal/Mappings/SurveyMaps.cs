using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHiberanteDal.Models;

namespace NHiberanteDal.Mappings
{
    public class SurveyModelMap : ClassMap<SurveyModel>
    {
        public SurveyModelMap()
        {
            Id(x => x.ID);
            Map(x => x.SurveyText).Not.Nullable();

            //Many
            HasMany(x => x.Questions).KeyColumns.Add("SurveyId").Not.LazyLoad();
        }
    }

    public class SurveyQuestionModelMap : ClassMap<SurveyQuestionModel>
    {
        public SurveyQuestionModelMap()
        {
            Id(x => x.ID);
            Map(x => x.QuestionText).Not.Nullable();
            Map(x => x.TimesSelected);
        }
    }
}
