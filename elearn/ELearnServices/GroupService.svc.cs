using System;
using System.Collections.Generic;
using System.Linq;
using NHiberanteDal.DTO;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using NHiberanteDal.DataAccess.QueryObjects;

namespace ELearnServices
{
    public class GroupService : IGroupService
    {
        public GroupService()
        {
            DTOMappings.Initialize();
        }

        public List<GroupModelDto> GetAllGroups()
        {
            var groups = new Repository<GroupModel>().GetAll().ToList();
            return GroupModelDto.Map(groups);
        }

        public GroupModelDto GetGroup(int id)
        {
            GroupModel group;
            using (var session = DataAccess.OpenSession())
            {
                group = session.Get<GroupModel>(id);
            }
            return GroupModelDto.Map(group);
        }

        public int AddGroup(GroupModelDto groupModelDto)
        {
            var groupModel = GroupModelDto.UnMap(groupModelDto);
            int id = -1;
            DataAccess.InTransaction(session =>
            {
                id = (int)session.Save(groupModel);
            });
            return id;
        }

        public bool DeleteGroup(GroupModelDto groupDto)
        {
            try
            {
                DataAccess.InTransaction(session => 
                    session.Delete(GroupModelDto.UnMap(groupDto)));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateGroup(GroupModelDto groupModelDto)
        {
            try
            {
                DataAccess.InTransaction(session => 
                    session.Update(GroupModelDto.UnMap(groupModelDto)));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IList<GroupTypeModelDto> GetGroupTypes()
        {
            return GroupTypeModelDto.Map(new Repository<GroupTypeModel>().GetAll().ToList());
        }

        public IList<GroupTypeModelDto> GetGroupTypeByName(string typeName)
        {
            List<GroupTypeModelDto> types;
            using (var session = DataAccess.OpenSession())
            {
                types = GroupTypeModelDto.Map((List<GroupTypeModel>)session.CreateQuery(new QueryGroupTypesByName(typeName).Query).List<GroupTypeModel>());
            }
            return types;
        }


        public bool AddProfileToGroup(int groupId, int profileId)
        {
            var group = new Repository<GroupModel>().GetById(groupId);
            var profile = new Repository<ProfileModel>().GetById(profileId);

            var index = -1;

            foreach (var p in group.Users)
            {
                if (p.ID == profile.ID)
                {
                    index = group.Users.IndexOf(p);
                    break;
                }
            }

            if (index == -1)
            {
                group.Users.Add(profile);
                if (new Repository<GroupModel>().Update(group))
                    return true;
            }
            return false;
        }

        public bool RemoveProfileFromGroup(int groupId, int profileId)
        {
            var group = new Repository<GroupModel>().GetById(groupId);
            var profile = new Repository<ProfileModel>().GetById(profileId);

            var index = -1;

            foreach (var p in group.Users)
            {
                if (p.ID == profile.ID)
                {
                    index = group.Users.IndexOf(p);
                    break;
                }
            }

            if (index >= 0)
            {
                group.Users.RemoveAt(index);
                if (new Repository<GroupModel>().Update(group))
                    return true;
            }
            return false;
        }
    }
}
