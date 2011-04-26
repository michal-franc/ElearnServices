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

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProfileService" in code, svc and config file together.
    public class ProfileService : IProfileService
    {

        public int AddProfile(ProfileModelDto profile,string password)
        {
            var profileModel = ProfileModelDto.UnMap(profile);
            int id = -1;
            DataAccess.InTransaction(session =>
                {
                    id = (int)session.Save(profileModel);
                });

            if (id != -1)
            {
                var status = CreateUser(profile.Uid.ToString(),password,profile.Email);
                if (status == MembershipCreateStatus.Success)
                    return id;
                else
                    return -1;
            }
            return -1;
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

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {   
            MembershipCreateStatus status;
            if(Membership.Provider.RequiresQuestionAndAnswer)
                Membership.Provider.CreateUser(userName, password, email, "this is sample question", "this is answer", true, null, out status);
            else
                Membership.Provider.CreateUser(userName, password, email, null, null, true, null, out status);
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
    }
}
