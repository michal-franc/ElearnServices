using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHiberanteDal.Models;

namespace NHiberanteDal.Mappings
{
    public class ForumModelMap : ClassMap<ForumModel>
    {
        public ForumModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Author).Not.Nullable();
            Map(x => x.Name).Not.Nullable();


            //Many
            HasMany(x=>x.Topics).KeyColumns.Add("ForumId");
        }

    }

    public class TopicModelMap : ClassMap<TopicModel>
    {
        public TopicModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Text).Not.Nullable();

            //Many
            HasMany(x => x.Posts).KeyColumns.Add("TopicId");
        }
    }

    public class PostModelMap : ClassMap<PostModel>
    {
        public PostModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Text).Not.Nullable();
        }
    }
}
