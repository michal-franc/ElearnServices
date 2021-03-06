﻿using FluentNHibernate.Mapping;
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
            HasManyToMany(x => x.Users).LazyLoad();      
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
            Map(x => x.DisplayName);
            Map(x => x.LoginName);
            Map(x=>x.Role);
            Map(x => x.Email).Not.Nullable();
            Map(x => x.IsActive).Default("FALSE");
            //Many
            HasMany(x=>x.Journals).Cascade.SaveUpdate().LazyLoad();
            HasMany(x=>x.FinishedTests).Cascade.SaveUpdate().LazyLoad();

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
            Map(x=>x.IsNew);
            Map(x=>x.Text);

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
            Map(x=>x.IsActive);

            //One
            References(x => x.Course).Not.Nullable().Not.LazyLoad();

            //Many
            HasMany(x => x.Marks).Cascade.All().Not.LazyLoad();

        }
    }

    public class MarkModelMap : ClassMap<JournalMarkModel>
    {
        public MarkModelMap()
        {
            Id(x => x.ID);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Value).Not.Nullable();
            Map(x => x.DateAdded).Not.Nullable();
        }
    }
}
