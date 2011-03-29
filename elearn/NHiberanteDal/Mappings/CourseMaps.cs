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
            Map(x => x.Name);
            Map(x => x.CreationDate);
            

            //One
            References(x => x.Group);
            References(x => x.CourseType);
            References(x => x.Forum);

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
                Map(x => x.TypeName);
            }
        }

        public class SurveyModelMap : ClassMap<SurveyModel>
        {
            public SurveyModelMap()
            {
                Id(x => x.ID);
                Map(x => x.SurveyText);

                //Many
                HasMany(x => x.Questions);
            }
        }

        public class SurveyQuestionModelMap : ClassMap<SurveyQuestionModel>
        {
            public SurveyQuestionModelMap()
            {
                Id(x => x.ID);
                Map(x => x.QuestionText);
                Map(x => x.TimesSelected);
            }
        }
    }
