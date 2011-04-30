using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;
using System.Web.Security;
using NHiberanteDal.DataAccess.QueryObjects;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProfileService" in code, svc and config file together.
    public class ProfileService : IProfileService
    {
        public ProfileService()
        {
            DTOMappings.Initialize();
        }

        public int AddProfile(ProfileModel profile)
        {
            int id = -1;
            DataAccess.InTransaction(session =>
                {
                    id = (int)session.Save(profile);
                });

            return id;
        }

        public ProfileModelDto GetProfile(int id)
        {
            ProfileModel profile = null;
            using (var session = DataAccess.OpenSession())
            {
                profile = session.Get<ProfileModel>(id);
            }
            return ProfileModelDto.Map(profile);
        }

        public List<ProfileModelDto> GetAllProfiles()
        {
            var profiles = new Repository<ProfileModel>().GetAll().ToList();
            return ProfileModelDto.Map(profiles);
        }

        public bool DeleteProfile(ProfileModelDto profile)
        {
            try
            {
                DataAccess.InTransaction(session =>
                    {
                        session.Delete(ProfileModelDto.UnMap(profile));
                    });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateProfile(ProfileModelDto profile)
        {
            try
            {
                DataAccess.InTransaction(session =>
                    {
                        session.Update(ProfileModelDto.UnMap(profile));
                    });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void UpdateRole(ProfileModelDto profile,bool createIfNotExist)
        {
            var role = profile.Role;
            var userName = profile.Name;
            if (!String.IsNullOrWhiteSpace(role))
            {
                if (Roles.RoleExists(role) && !Roles.IsUserInRole(userName, role))
                {
                    DeleteUserFromRoles(userName);
                    Roles.AddUserToRole(userName, role);
                }
                else if (!Roles.RoleExists(role) && createIfNotExist)
                {
                    Roles.CreateRole(role);
                    Roles.AddUserToRole(userName, role);
                }
            }
            else
            {
                DeleteUserFromRoles(userName);
            }
        }

        private void DeleteUserFromRoles(string userName)
        {
            var roles = Roles.GetRolesForUser();
            foreach (string r in roles)
            {
                Roles.RemoveUserFromRole(userName, r);
            }
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            var profile = new ProfileModel() { Name=userName, Email=email };
            this.AddProfile(profile);

            MembershipCreateStatus status;
            if(Membership.Provider.RequiresQuestionAndAnswer)
                Membership.Provider.CreateUser(profile.Name, password, email, "this is sample question", "this is answer", true, null, out status);
            else
                Membership.Provider.CreateUser(profile.Name, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ValidateUser(string userName, string password)
        {
            return Membership.Provider.ValidateUser(userName, password);
        }

        public void ResetPassword(string userName)
        {
            string newPassword = Membership.Provider.ResetPassword(userName, null);
            //email newPassword
        }



        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            return Membership.Provider.ChangePassword(userName, oldPassword, newPassword);
        }

        public int GetMinPasswordLength()
        {
            return Membership.Provider.MinRequiredPasswordLength;

        }

        public ProfileModelDto GetByName(string userName)
        {
            return ProfileModelDto.Map(new Repository<ProfileModel>().GetByQueryObject(new QueryProfilesByName(userName)).FirstOrDefault());
        }


        public bool IsUserInRoles(string userName,string[] roles)
        {
            foreach (string s in roles)
            {
                if (Roles.IsUserInRole(userName,s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
