using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using FluentNHibernate.Mapping;

namespace NHiberanteDal.Mappings
{
    public class ShoutboxModelMap : ClassMap<ShoutboxModel>
    {
        public ShoutboxModelMap()
        {
            Id(x => x.ID);

            //Many
            HasMany(x => x.Messages).Cascade.All().Not.LazyLoad().KeyColumns.Add("ShoutBoxId");

        }
    }

    public class ShoutBoxMessageModelMap : ClassMap<ShoutBoxMessageModel>
    {

        public ShoutBoxMessageModelMap()
        {
            Id(x => x.ID);
            Map(x => x.ShoutBoxId).Not.Nullable();
            Map(x => x.Author).Not.Nullable();
            Map(x => x.Message).Not.Nullable();
            Map(x => x.TimePosted).Not.Nullable();
        } 
    }
}
