using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ProfileService" in code, svc and config file together.
    public class ProfileService : IProfileService
    {

        public int AddProfile(ProfileModelDto profile)
        {
            var profileModel = ProfileModelDto.UnMap(profile);
            int id = -1;
            DataAccess.InTransaction(session =>
                {
                    id = (int)session.Save(profileModel);
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
    }
}
