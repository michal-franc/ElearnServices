using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Models;

namespace NHiberanteDal.Mappings
{
    public class ContentMap : ClassMap<ContentModel>
    {
        public ContentMap()
        {
            Id(x => x.ID);
            Map(x => x.IDCourse);
            Map(x => x.Name);
            Map(x => x.Text);
            Map(x => x.DownloadNumber);
            Map(x => x.CreationDate);
            Map(x => x.EditDate);
            Map(x => x.ContentUrl);


            //References
            References(x => x.Type);                      
        }
    }

    public class ContentTypeMap : ClassMap<ContentTypeModel>
    {
        public ContentTypeMap()
        {
            Id(x => x.ID);
            Map(x => x.TypeName);
        }
    }
}
