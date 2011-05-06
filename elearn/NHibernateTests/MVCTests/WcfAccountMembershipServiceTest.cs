using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using elearn.Models;
using Rhino.Mocks;
using elearn.ProfileService;
using System.Web.Security;

namespace NHibernateTests.MVCTests
{
    [TestFixture]
    class WcfAccountMembershipServiceTest
    {
        WcfAccountMembershipService _service;
        IProfileService _profileService;
        MockRepository _mock;

        [SetUp]
        public void SetUp()
        {
            _mock = new MockRepository();
            _profileService = _mock.DynamicMock<IProfileService>();
            _service = new WcfAccountMembershipService(_profileService);
        }

        [Test]
        public void Create_user_creates_profile_in_membership_base_and_mainDB()
        {
            #region Arrange

            using (_mock.Record())
            {

            }

            #endregion

            #region Act

            using (_mock.Playback())
            {
                var status =_service.CreateUser("test", "test", "test@test.com");
            }

            #endregion

            #region Assert
            #endregion
        }



        [Test]
        public void Create_user_by_default_sets_role_to_basicuser()
        {
            #region Arrange

            using (_mock.Record())
            {
            }
            #endregion

            #region Act
            using (_mock.Playback())
            {
                Assert.Fail();
            }

            #endregion

            #region Assert
            #endregion
        }
		

        [Test]
        public void If_aspnetdb_create_user_failes_then_profiledb_is_not_commited()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }
				
    }
}
