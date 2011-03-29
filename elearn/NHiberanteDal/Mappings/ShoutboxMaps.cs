using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using FluentNHibernate.Mapping;

namespace NHiberanteDal.Mappings
{
    public class ShoutboxModelMap : ClassMap<ShoutboxModel>
    {
        public ShoutboxModelMap()
        {
            Id(x => x.ID);
            
            //One
            References(x => x.CourseOwner).Not.Nullable();

            //Many
            HasMany(x=>x.Messages);

        }
    }

    public class ShoutBoxMessageModelMap : ClassMap<ShoutBoxMessageModel>
    {

        public ShoutBoxMessageModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Author).Not.Nullable();
            Map(x => x.Message).Not.Nullable();
            Map(x => x.TimePosted).Not.Nullable();
        } 
    }
}
