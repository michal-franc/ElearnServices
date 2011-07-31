using FluentNHibernate.Mapping;
using NHiberanteDal.Models;

namespace NHiberanteDal.Mappings 
{
    public class LearningMaterialMaps : ClassMap<LearningMaterialModel>
    {
        public LearningMaterialMaps()
        {
            Id(x => x.ID);
            Map(x=>x.CreationDate);
            Map(x => x.Description).Length(4001);
            Map(x => x.Title);
            Map(x => x.IconName);
            Map(x => x.Goals).Length(4001);
            Map(x => x.Level);
            Map(x => x.UpdateDate);
            Map(x => x.VersionNumber);
            Map(x => x.Summary).Length(4001);
            Map(x => x.Links).Length(4001);


            //Many
            HasMany(x => x.Tests).Cascade.All().LazyLoad();
            HasMany(x => x.Sections).Cascade.All().LazyLoad();
            HasMany(x => x.Files).Cascade.All().LazyLoad();

        }
    }


    public class SectionMaps : ClassMap<SectionModel>
    {
        public SectionMaps()
        {
            Id(x => x.ID);
            Map(x => x.IconName);
            Map(x => x.Title);
            Map(x => x.Text).Length(4001);
        }
    }

    public class FileMaps : ClassMap<FileModel>
    {
        public FileMaps()
        {
            Id(x => x.ID);
            Map(x => x.FileName);
            Map(x => x.Address);
        }
    }
}
