using NHiberanteDal.Models;
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
            Map(x => x.Description);
            Map(x => x.ShortDescription);
            Map(x => x.News);
            Map(x=>x.Password).Nullable();
            
            //One
            References(x => x.Group).Not.Nullable().Cascade.All().LazyLoad();
            References(x => x.CourseType).Not.Nullable().Not.LazyLoad();
            References(x => x.ShoutBox).Not.Nullable().Cascade.All().LazyLoad();

            HasMany(x => x.Tests).KeyColumns.Add("CourseId").Cascade.SaveUpdate().LazyLoad();
            HasMany(x => x.LearningMaterials).KeyColumns.Add("CourseId").Cascade.SaveUpdate().LazyLoad();
            Cache.ReadWrite();
        }
    }

    public class CourseTypeModelMap : ClassMap<CourseTypeModel>
    {
        public CourseTypeModelMap()
        {
            Id(x => x.ID);
            Map(x => x.TypeName).Not.Nullable();
            Cache.ReadOnly();
        }
    }
 }
