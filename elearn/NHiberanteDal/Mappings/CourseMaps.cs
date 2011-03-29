using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using FluentNHibernate.Mapping;


namespace NHiberanteDal.Mappings
{
    public class CourseModelMap : ClassMap<CourseModel>
    {
        public CourseModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Logo);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.CreationDate).Not.Nullable();
            

            //One
            References(x => x.Group).Not.Nullable();
            References(x => x.CourseType).Not.Nullable();
            References(x => x.Forum).Not.Nullable();

            //Many
            HasMany(x => x.Contents);
            HasMany(x => x.Surveys);
            HasMany(x => x.Tests);
        }
    }

        public class CourseTypeModelMap : ClassMap<CourseTypeModel>
        {
            public CourseTypeModelMap()
            {
                Id(x => x.ID);
                Map(x => x.TypeName).Not.Nullable();
            }
        }

        public class SurveyModelMap : ClassMap<SurveyModel>
        {
            public SurveyModelMap()
            {
                Id(x => x.ID);
                Map(x => x.SurveyText).Not.Nullable();

                //Many
                HasMany(x => x.Questions);
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
