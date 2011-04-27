using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using NHiberanteDal.Models;

namespace NHiberanteDal.Mappings
{
    public class GroupModelMap : ClassMap<GroupModel>
    {

        public GroupModelMap()
        {
            Id(x => x.ID);
            Map(x => x.GroupName).Not.Nullable();

            //One
            References(x => x.GroupType).Not.LazyLoad();

            //Many

            HasMany(x=>x.Users);           
        }
    }

    public class GroupTypeModelMap :ClassMap<GroupTypeModel>
    {
        public GroupTypeModelMap()
        {
            Id(x=>x.ID);
            Map(x => x.TypeName).Not.Nullable();

        }
    }

    public class ProfileModelMap : ClassMap<ProfileModel>
    {
        public ProfileModelMap()
	    {
            Id(x=>x.ID);
            Map(x => x.Name);
            Map(x=>x.Role);
            Map(x => x.Email).Not.Nullable();
            Map(x => x.IsActive).Default("FALSE");
            //Many
            HasMany(x=>x.Journals).Cascade.SaveUpdate().Not.LazyLoad();

	    }
    }

    public class ProfileTypeModelMap : ClassMap<ProfileTypeModel>
    {
        public ProfileTypeModelMap()
        {
            Id(x => x.ID);
            Map(x => x.TypeName);
        }
    }

    public class PrivateMessageModelMap :ClassMap<PrivateMessageModel>
    {
        public PrivateMessageModelMap()
        {
            Id(x => x.ID);

            //One
            References(x => x.Sender).Not.Nullable().Not.LazyLoad();
            References(x => x.Receiver).Not.Nullable().Not.LazyLoad();
        }
    }

    public class JournalModelMap : ClassMap<JournalModel>
    {
        public JournalModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Not.Nullable();
            Map(x=>x.AverageMark);

            //One
            References(x => x.Course).Not.Nullable().Not.LazyLoad();

            //Many
            HasMany(x => x.Marks).KeyColumns.Add("JournalId");

        }
    }

    public class MarkModelMap : ClassMap<JournalMarkModel>
    {
        public MarkModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Value).Not.Nullable();
        }
    }
}
