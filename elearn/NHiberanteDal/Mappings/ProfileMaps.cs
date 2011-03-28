using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Models;

namespace NHiberanteDal.Mappings
{
    public class GroupModelMap : ClassMap<GroupModel>
    {

        public GroupModelMap()
        {
            Id(x => x.ID);
            Map(x => x.GroupName);

            //Many

            HasMany(x=>x.Users);
            
        }
    }

    public class GroupTypeModelMap :ClassMap<GroupTypeModel>
    {
        public GroupTypeModelMap()
        {
            Id(x=>x.ID);
            Map(x=>x.TypeName);

        }
    }

    public class ProfileModel
    {
        int ID;
        // Linked with Forms Authentication
    }

    public class ProfileTypeModel
    {
        int ID;
        string TypeName;
    }

    public class PrivateMessageModelMap :ClassMap<PrivateMessageModel>
    {
        public PrivateMessageModelMap()
        {
            Id(x => x.ID);

            //One
            References(x=>x.Sender);
            References(x=>x.Receiver);
        }
    }

    public class JournalModelMap : ClassMap<JournalModel>
    {
        public JournalModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x=>x.AverageMark);

            //One
            References(x=>x.Owner);

            //Many
            HasMany(x=>x.Marks);

        }
    }

    public class MarkModelMap : ClassMap<JournalMarkModel>
    {
        public MarkModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.Value);
        }
    }
}
