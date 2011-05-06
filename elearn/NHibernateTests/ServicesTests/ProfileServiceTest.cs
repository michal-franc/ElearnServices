using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ELearnServices;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using NHiberanteDal.DTO;
using Rhino.Mocks;


namespace NHibernateTests.ServicesTests
{

    [TestFixture]
    class ProfileServiceTest : InMemoryWithSampleData
    {
        MockRepository mocker = new MockRepository();

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
            var profile = new ProfileModelDto() { Name = "new profile", Email = "test@test.com" };
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
        public void Can_set_profile_as_inactive_by_id()
        {
            #region Arrange

            #endregion

            #region Act

            var updateOk = new ProfileService().SetAsInactive(1);

            bool isActive = true;
            DataAccess.InTransaction(session =>
                {
                    isActive = session.Get<ProfileModel>(1).IsActive;
                });

            #endregion

            #region Assert
            Assert.IsTrue(updateOk);
            Assert.IsFalse(isActive);
            #endregion
        }

        [Test]
        public void Can_set_profile_as_inactive_by_userName()
        {
            #region Arrange

            #endregion

            #region Act

            var updateOk = new ProfileService().SetAsInactiveByName("test");

            bool isActive = true;
            DataAccess.InTransaction(session =>
            {
                isActive = session.Get<ProfileModel>(1).IsActive;
            });

            #endregion

            #region Assert
            Assert.IsTrue(updateOk);
            Assert.IsFalse(isActive);
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


        [Test]
        public void Updates_Role_deletes_user_from_existing_roles()
        {
            #region Arrange

            var roleProvider = mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto() { Role="admin", Name="user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (mocker.Record())
            {
                Expect.Call(roleProvider.RoleExists("admin")).Return(true);
                Expect.Call(roleProvider.IsUserInRole("user", "admin")).Return(false);
                Expect.Call(roleProvider.GetRolesForUser("user")).Return(new string[] { "unusedRole" });
                Expect.Call(delegate {
                    roleProvider.RemoveUserFromRole("user", "unusedRole");
                }).Repeat.Once();
                Expect.Call(delegate 
                {
                    roleProvider.AddUserToRole("user", "admin");
                });
            }

            using (mocker.Playback())
            {
                service.UpdateRole(profile, false);
            }

            #endregion

            #region Assert
            #endregion
        }


        [Test]
        public void Is_No_Role_Create_New_Role()
        {
            #region Arrange

            var roleProvider = mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto() { Role = "admin", Name = "user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (mocker.Record())
            {
                Expect.Call(roleProvider.RoleExists("admin")).Return(false);
                Expect.Call(delegate {
                    roleProvider.CreateRole("admin");
                });
                Expect.Call(roleProvider.IsUserInRole("user", "admin")).Repeat.Never();
                Expect.Call(roleProvider.GetRolesForUser("user")).Return(new string[] { "unusedRole" });
                Expect.Call(delegate
                {
                    roleProvider.RemoveUserFromRole("user", "unusedRole");
                }).Repeat.Once();
                Expect.Call(delegate
                {
                    roleProvider.AddUserToRole("user", "admin");
                });
            }

            using (mocker.Playback())
            {
                service.UpdateRole(profile, true);
            }

            #endregion
        }


        [Test]
        public void If_No_Role_Dont_Update()
        {
            #region Arrange

            var roleProvider = mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto() { Role = "admin", Name = "user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (mocker.Record())
            {
                Expect.Call(roleProvider.RoleExists("admin")).Return(false);
                Expect.Call(delegate
                {
                    roleProvider.AddUserToRole("user", "admin");
                }).Repeat.Never();
                Expect.Call(delegate
                {
                    roleProvider.RemoveUserFromRole("user", "unusedRole");
                }).Repeat.Never().IgnoreArguments();
            }

            using (mocker.Playback())
            {
                service.UpdateRole(profile, false);
            }

            #endregion
        }

        [Test]
        public void If_Role_Parameter_Empty_Reset_User_Roles()
        {
            #region Arrange

            var roleProvider = mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto() { Role = "", Name = "user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (mocker.Record())
            {
                Expect.Call(roleProvider.GetRolesForUser("user")).Return(new string[] { "unusedRole" });
                Expect.Call(delegate
                {
                    roleProvider.RemoveUserFromRole("user", "unusedRole");
                });
            }

            using (mocker.Playback())
            {
                service.UpdateRole(profile, false);
            }

            #endregion
        }
							
				
    }
}
