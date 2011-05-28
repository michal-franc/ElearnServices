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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public GroupService()
        {
            DtoMappings.Initialize();
        }

        public List<GroupModelDto> GetAllGroups()
        {
            try
            {
                var groups = new Repository<GroupModel>().GetAll().ToList();
                return GroupModelDto.Map(groups);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetAllGroups - {0}", ex.Message);
                return new List<GroupModelDto>();
            }
        }

        public GroupModelDto GetGroup(int id)
        {
            try
            {
                GroupModel group;
                using (var session = DataAccess.OpenSession())
                {
                    group = session.Get<GroupModel>(id);
                }
                return GroupModelDto.Map(group);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetGroup - {0}", ex.Message);
                return null;
            }
        }

        public int AddGroup(GroupModelDto groupModelDto)
        {
            try
            {
                var groupModel = GroupModelDto.UnMap(groupModelDto);
                int id = -1;
                DataAccess.InTransaction(session =>
                {
                    id = (int)session.Save(groupModel);
                });
                return id;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.AddGroup - {0}", ex.Message);
                return -1;
            }
        }

        public bool DeleteGroup(GroupModelDto groupDto)
        {
            try
            {
                DataAccess.InTransaction(session => 
                    session.Delete(GroupModelDto.UnMap(groupDto)));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.DeleteGroup - {0}", ex.Message);
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
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.UpdateGroup - {0}", ex.Message);
                return false;
            }
        }

        public IList<GroupTypeModelDto> GetGroupTypes()
        {
            try
            {
                return GroupTypeModelDto.Map(new Repository<GroupTypeModel>().GetAll().ToList());
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetGroupTypes - {0}", ex.Message);
                return new List<GroupTypeModelDto>();
            }
        }

        public IList<GroupTypeModelDto> GetGroupTypeByName(string typeName)
        {
            try
            {
                List<GroupTypeModelDto> types;
                using (var session = DataAccess.OpenSession())
                {
                    types = GroupTypeModelDto.Map((List<GroupTypeModel>)session.CreateQuery(new QueryGroupTypesByName(typeName).Query).List<GroupTypeModel>());
                }
                return types;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetGroupTypeByName - {0}", ex.Message);
                return new List<GroupTypeModelDto>();
            }
        }


        public bool AddProfileToGroup(int groupId, int profileId)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.AddProfileToGroup - {0}", ex.Message);
                return false;
            }
        }

        public bool RemoveProfileFromGroup(int groupId, int profileId)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.RemoveProfileFromGroup - {0}", ex.Message);
                return false;
            }
        }
    }
}
