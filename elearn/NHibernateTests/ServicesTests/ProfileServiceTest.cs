using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ELearnServices;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using NHiberanteDal.DTO;


namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    class ProfileServiceTest : InMemoryWithSampleData
    {
        [Test]
        public void Can_Get_All_Profiles()
        {
            #region Arrange
            var profile = new ProfileModel() { Name = "test1", Email = "test@test.com", IsActive = true };
            DataAccess.InTransaction(session =>
                {
                    session.Save(profile);
                });
            #endregion

            #region Act

            var profiles = new ProfileService().GetAllProfiles();

            #endregion

            #region Assert
            Assert.That(profiles.First(),Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(profiles.Count,Is.EqualTo(2));
            #endregion
        }


        [Test]
        public void Can_Get_Profile_by_UserName()
        {
            #region Arrange
            var profile = new ProfileModel() { Name = "test profile", Email = "test@test.com", IsActive = true };
            #endregion

            #region Act

            using (var session = DataAccess.OpenSession())
            {
                session.Save(profile);
            }
            var returnedProfile = new ProfileService().GetByName("test profile");
            #endregion

            #region Assert
            Assert.That(returnedProfile,Is.Not.Null);
            Assert.That(returnedProfile.Name,Is.EqualTo("test profile"));
            #endregion
        }
				

        [Test]
        public void Can_Get_Profile()
        {
            #region Arrange
            #endregion

            #region Act
            var profile = new ProfileService().GetProfile(1);
            #endregion

            #region Assert
            Assert.That(profile, Is.Not.Null);
            Assert.That(profile.Name,Is.EqualTo("test"));
            #endregion
        }

        [Test]
        public void Can_Add_Profile()
        {
            #region Arrange
            var profile = new ProfileModel() { Name = "new profile", Email = "test@test.com", IsActive = true };
            #endregion

            #region Act

            int id = new ProfileService().AddProfile(profile);
            ProfileModel profileModel = null;
            DataAccess.InTransaction(session =>
            {
                profileModel = session.Get<ProfileModel>(id);
            });
            #endregion

            #region Assert
            Assert.That(id,Is.GreaterThan(-1));
            Assert.That(profileModel,Is.Not.Null);
            Assert.That(profileModel.Name, Is.EqualTo("new profile"));
            #endregion
        }

        [Test]
        public void Can_Update_Profile()
        {
            #region Arrange
            ProfileModel profile = null;
            DataAccess.InTransaction(session =>
                {
                    profile = session.Get<ProfileModel>(1);
                });
            profile.Name = "update test";
            #endregion

            #region Act

            bool updateOk = new ProfileService().UpdateProfile(ProfileModelDto.Map(profile));

            #endregion
            ProfileModel testProfile = null;
            DataAccess.InTransaction(session =>
            {
                testProfile = session.Get<ProfileModel>(1);
            });

            #region Assert
            Assert.That(updateOk,Is.True);
            Assert.That(testProfile.Name,Is.EqualTo("update test"));
            #endregion
        }


        [Test]
        public void Can_Delete_Profile()
        {
            #region Arrange
            var profile = new ProfileModel() { Name = "delete test", Email = "test@test.com", IsActive = true };
            int id = -1;
            DataAccess.InTransaction(session =>
            {
                id = (int)session.Save(profile);
            });
            #endregion

            #region Act
            var profileDto = ProfileModelDto.Map(profile);
            profileDto.ID = id;
            bool deleteOk = new ProfileService().DeleteProfile(profileDto);

            IList<ProfileModel> profiles = null;

            DataAccess.InTransaction(session =>
            {
                profiles = session.CreateCriteria(typeof(ProfileModel)).List<ProfileModel>();
            });

            #endregion

            #region Assert
            Assert.That(deleteOk, Is.True);
            Assert.That(profiles.Count, Is.EqualTo(1));
            #endregion
        }				
    }
}
