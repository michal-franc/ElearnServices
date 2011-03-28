using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using FluentNHibernate.Mapping;

namespace NHiberanteDal.Mappings
{
    public class ForumModelMap : ClassMap<ForumModel>
    {
        public ForumModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Author);
            Map(x => x.Name);
        }

    }

    public class TopicModelMap : ClassMap<TopicModel>
    {
        public TopicModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Text);
        }
    }

    public class PostModelMap : ClassMap<PostModel>
    {
        public PostModelMap()
        {
            Id(x => x.ID);
            Map(x=>x.Text);
        }
    }
}
