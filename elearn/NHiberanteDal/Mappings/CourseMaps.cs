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
            HasMany(x => x.Contents).KeyColumns.Add("CourseId");
            HasMany(x => x.Surveys).KeyColumns.Add("CourseId"); ;
            HasMany(x => x.Tests).KeyColumns.Add("CourseId"); ;
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


    }
