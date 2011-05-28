using System.Collections.Generic;
using System.Linq;
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
        readonly MockRepository _mocker = new MockRepository();

        [Test]
        public void Can_get_all_profiles()
        {
            #region Arrange
            var profile = new ProfileModel { Name = "test1", Email = "test@test.com", IsActive = true };
            DataAccess.InTransaction(session => session.Save(profile));
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
        public void Can_get_profile_by_username()
        {
            #region Arrange
            var profile = new ProfileModel { Name = "test profile", Email = "test@test.com", IsActive = true };
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
        public void Can_get_profile()
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
        public void Can_add_profile()
        {
            #region Arrange
            var profile = new ProfileModelDto { Name = "new profile", Email = "test@test.com" };
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
        public void Can_set_profile_as_inactive_by_username()
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
        public void Can_update_profile()
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
        public void Can_check_if_user_is_active()
        {
            #region Arrange
            #endregion

            #region Act

            bool active = new ProfileService().IsActiveByName("test");

            #endregion

            #region Assert
            Assert.IsTrue(active);
            #endregion
        }
				

        [Test]
        public void Can_delete_profile()
        {
            #region Arrange
            var profile = new ProfileModel { Name = "delete test", Email = "test@test.com", IsActive = true };
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
        public void Updates_role_deletes_user_from_existing_roles()
        {
            #region Arrange

            var roleProvider = _mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto { Role="admin", Name="user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (_mocker.Record())
            {
                Expect.Call(roleProvider.RoleExists("admin")).Return(true);
                Expect.Call(roleProvider.IsUserInRole("user", "admin")).Return(false);
                Expect.Call(roleProvider.GetRolesForUser("user")).Return(new[] { "unusedRole" });
                Expect.Call(() => roleProvider.RemoveUserFromRole("user", "unusedRole")).Repeat.Once();
                Expect.Call(() => roleProvider.AddUserToRole("user", "admin"));
            }

            using (_mocker.Playback())
            {
                service.UpdateRole(profile, false);
            }

            #endregion

            #region Assert
            #endregion
        }


        [Test]
        public void Is_no_role_create_new_role()
        {
            #region Arrange

            var roleProvider = _mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto { Role = "admin", Name = "user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (_mocker.Record())
            {
                Expect.Call(roleProvider.RoleExists("admin")).Return(false);
                Expect.Call(() => roleProvider.CreateRole("admin"));
                Expect.Call(roleProvider.IsUserInRole("user", "admin")).Repeat.Never();
                Expect.Call(roleProvider.GetRolesForUser("user")).Return(new[] { "unusedRole" });
                Expect.Call(() => roleProvider.RemoveUserFromRole("user", "unusedRole")).Repeat.Once();
                Expect.Call(() => roleProvider.AddUserToRole("user", "admin"));
            }

            using (_mocker.Playback())
            {
                service.UpdateRole(profile, true);
            }

            #endregion
        }


        [Test]
        public void If_no_role_dont_update()
        {
            #region Arrange

            var roleProvider = _mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto { Role = "admin", Name = "user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (_mocker.Record())
            {
                Expect.Call(roleProvider.RoleExists("admin")).Return(false);
                Expect.Call(() => roleProvider.AddUserToRole("user", "admin")).Repeat.Never();
                Expect.Call(() => roleProvider.RemoveUserFromRole("user", "unusedRole")).Repeat.Never().IgnoreArguments();
            }

            using (_mocker.Playback())
            {
                service.UpdateRole(profile, false);
            }

            #endregion
        }

        [Test]
        public void If_role_parameter_empty_reset_user_roles()
        {
            #region Arrange

            var roleProvider = _mocker.DynamicMock<IRoleProvider>();
            var profile = new ProfileModelDto { Role = "", Name = "user" };
            var service = new ProfileService(roleProvider);

            #endregion

            #region Act
            using (_mocker.Record())
            {
                Expect.Call(roleProvider.GetRolesForUser("user")).Return(new[] { "unusedRole" });
                Expect.Call(() => roleProvider.RemoveUserFromRole("user", "unusedRole"));
            }

            using (_mocker.Playback())
            {
                service.UpdateRole(profile, false);
            }

            #endregion
        }
							
				
    }
}
