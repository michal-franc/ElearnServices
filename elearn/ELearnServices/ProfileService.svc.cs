using System;
using System.Collections.Generic;
using System.Linq;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;
using System.Web.Security;
using NHiberanteDal.DataAccess.QueryObjects;

namespace ELearnServices
{
    public class ProfileService : IProfileService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly IRoleProvider _roleProvider;

        public ProfileService() : this(new MembershipRoleProvider())
        {
            Logger.Info("Created ProfileService");
        }

        public ProfileService(IRoleProvider provider)
        {
            _roleProvider = provider;
            DtoMappings.Initialize();
        }

        public int AddProfile(ProfileModelDto profile)
        {
            try
            {
                int id = -1;
                DataAccess.InTransaction(session =>
                    {
                        id = (int)session.Save(ProfileModelDto.UnMap(profile));
                    });

                return id;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.AddProfile - {0}", ex.Message);
                return -1;
            }
        }

        public ProfileModelDto GetProfile(int id)
        {
            try
            {
                ProfileModel profile;
                using (var session = DataAccess.OpenSession())
                {
                    profile = session.Get<ProfileModel>(id);
                }
                return ProfileModelDto.Map(profile);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.GetProfile- {0}", ex.Message);
                return null;
            }
        }

        public List<ProfileModelDto> GetAllProfiles()
        {
            try
            {
                var profiles = new Repository<ProfileModel>().GetAll().ToList();
                return ProfileModelDto.Map(profiles);

            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.GetAllProfiles - {0}", ex.Message);
                return null;
            }
        }

        public bool DeleteProfile(ProfileModelDto profile)
        {
            try
            {
                DataAccess.InTransaction(session => session.Delete(ProfileModelDto.UnMap(profile)));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.DeleteProfile - {0}", ex.Message);
                return false;
            }
        }

        public bool UpdateProfile(ProfileModelDto profile)
        {
            try
            {
                DataAccess.InTransaction(session => session.Update(ProfileModelDto.UnMap(profile)));
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.UpdateProfile - {0}", ex.Message);
                return false;
            }
        }

        public bool UpdateRole(ProfileModelDto profile,bool createIfNotExist)
        {
            var role = profile.Role;
            var userName = profile.Name;
            try
            {
                if (!String.IsNullOrWhiteSpace(role))
                {

                    if (_roleProvider.RoleExists(role) && !_roleProvider.IsUserInRole(userName, role))
                    {
                        DeleteUserFromRoles(userName);
                        _roleProvider.AddUserToRole(userName, role);
                    }
                    else if (!_roleProvider.RoleExists(role) && createIfNotExist)
                    {
                        _roleProvider.CreateRole(role);
                        DeleteUserFromRoles(userName);
                        _roleProvider.AddUserToRole(userName, role);
                    }

                }
                else
                {
                    DeleteUserFromRoles(userName);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.UpdateRole - {0}", ex.Message);
                return false;
            }
        }

        private void DeleteUserFromRoles(string userName)
        {
            try
            {
                var roles = _roleProvider.GetRolesForUser(userName);
                foreach (string r in roles)
                {
                    _roleProvider.RemoveUserFromRole(userName, r);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.DeleteUserFromRoles - {0}", ex.Message);
            }
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            try
            {
                var profile = new ProfileModelDto { Name = userName, Email = email, Role = "basicuser" };
                AddProfile(profile);
                UpdateRole(profile, false);

                MembershipCreateStatus status;
                if (Membership.Provider.RequiresQuestionAndAnswer)
                    Membership.Provider.CreateUser(profile.Name, password, email, "this is sample question", "this is answer", true, null, out status);
                else
                    Membership.Provider.CreateUser(profile.Name, password, email, null, null, true, null, out status);
                return status;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.CreateUser - {0}", ex.Message);
                return MembershipCreateStatus.ProviderError;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            try
            {
                return Membership.Provider.ValidateUser(userName, password);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.ValidateUser - {0}", ex.Message);
                return false;
            }
        }

        public void ResetPassword(string userName)
        {
            try
            {
                Membership.Provider.ResetPassword(userName, null);
                //email newPassword

            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.ResetPassword - {0}", ex.Message);
            }
        }


        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            try
            {
                return Membership.Provider.ChangePassword(userName, oldPassword, newPassword);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.ChangePassword - {0}", ex.Message);
                return false;
            }
        }

        public int GetMinPasswordLength()
        {
            try
            {
                return Membership.Provider.MinRequiredPasswordLength;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.GetMinPasswodLength - {0}", ex.Message);
                return -1;
            }
        }

        public ProfileModelDto GetByName(string userName)
        {
            try
            {
                return ProfileModelDto.Map(new Repository<ProfileModel>().GetByQueryObject(new QueryProfilesByName(userName)).FirstOrDefault());
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.GetByName - {0}", ex.Message);
                return null;
            }
        }


        public bool IsUserInRoles(string userName,string[] roles)
        {
            try
            {
                return roles.Any(s => _roleProvider.IsUserInRole(userName, s));

            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.IsUserInRoles - {0}", ex.Message);
                return false;
            }
        }

        public bool IsActiveByName(string userName)
        {
            try
            {
                var profile = new Repository<ProfileModel>().GetByQueryObject(new QueryProfilesByName(userName)).FirstOrDefault();
                return profile != null && profile.IsActive;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.IsActiveByName - {0}", ex.Message);
                return false;
            }
        }


        public  string[] GetAllRoles()
        {
            try
            {
                return _roleProvider.GetAllRoles();
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.GetAllRoles - {0}", ex.Message);
                return new string[0];
            }
        }

        public bool SetAsInactive(int id)
        {
            try
            {
                DataAccess.InTransaction(session =>
                {
                    var profile = session.Get<ProfileModel>(id);
                    profile.IsActive = false;
                    session.Update(profile);
                });
            return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.SetAsInactive - {0}", ex.Message);
                return false;
            }
        }

        public bool SetAsInactiveByName(string userName)
        {
            try
            {
                DataAccess.InTransaction(session =>
                {
                    var profile = new Repository<ProfileModel>().GetByQueryObject(new QueryProfilesByName(userName)).FirstOrDefault();
                    if (profile != null)
                    {
                        profile.IsActive = false;
                        session.Update(profile);
                    }
                });
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : ProfileService.SetAsInactiveByName - {0}", ex.Message);
                return false;
            }
        }


        public bool AddRole(string roleName)
        {
            if (_roleProvider.RoleExists(roleName))
                return false;
            _roleProvider.CreateRole(roleName);
            return true;
        }
    }
}
