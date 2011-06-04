using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHiberanteDal.Models;

//todo : add conventions to nhibernate mappings very important beacusae nhibernate is  creating weird FK
namespace NHiberanteDal.Mappings
{
    public class ContentMap : ClassMap<ContentModel>
    {
        public ContentMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Text);
            Map(x => x.DownloadNumber);
            Map(x => x.CreationDate).Default(DateTime.Now.ToShortDateString()).Not.Nullable();
            Map(x => x.EditDate);
            Map(x => x.ContentUrl).Not.Nullable();


            //References
            References(x => x.Type).Not.Nullable().Not.LazyLoad();                      
        }
    }

    public class ContentTypeMap : ClassMap<ContentTypeModel>
    {
        public ContentTypeMap()
        {
            Id(x => x.ID);
            Map(x => x.TypeName).Not.Nullable();
        }
    }
}
