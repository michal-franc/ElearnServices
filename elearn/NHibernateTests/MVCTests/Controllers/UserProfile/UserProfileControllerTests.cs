using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using elearn.Controllers;
using Rhino.Mocks;
using NHiberanteDal.DTO;
using elearn.ProfileService;
using System.Web.Mvc;
using MvcContrib.TestHelper;

namespace NHibernateTests.MVCTests.Controllers.UserProfile
{

    class BaseTest
    {
        protected MockRepository _mock;
        protected ProfileModelDto _profile;
        protected ProfileModelDto[] _profileList;

        [SetUp]
        public void SetUp()
        {
            _mock = new MockRepository();
            _profile = new ProfileModelDto() { ID = 1, Email = "test@test.com", Role = "admin", Name = "testuser" };
            _profileList = new ProfileModelDto[]
                {
                    new ProfileModelDto(){ ID=1},
                    new ProfileModelDto(){ ID=2}
                };

        }
    }

    [TestFixture]
    class Index : BaseTest
    {
        [Test]
        public void Gets_profile_of_current_user_redirects_to_details_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var userProfileController = new UserProfileController(profileService);
            #endregion

            #region Act
            var   redirect = (RedirectToRouteResult)userProfileController.Index();
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details");
            #endregion
        }			
    }

    [TestFixture]
    class Details : BaseTest
    {
        
        [Test]
        public void GetsProfileByName_then_returns_default_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var userProfileController = new UserProfileController(profileService);
            userProfileController.UserName = "test";

            using (_mock.Record())
            {
                Expect.Call(profileService.GetByName("test")).Return(_profile);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)userProfileController.Details();
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            #endregion
        }
				
    }
    [TestFixture]
    class Delete : BaseTest
    {
		[Test]
		public void Gets_profile_of_current_user_and_set_as_inactive_then_redirects_to_log_off_action()
		{
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var userProfileController = new UserProfileController(profileService);
            userProfileController.UserName = "test";

            using (_mock.Record())
            {
                Expect.Call(profileService.SetAsInactiveByName("test")).Return(true);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect = null;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)userProfileController.Delete();
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("LogOff").ToController("Account");
            #endregion
		}



        [Test]
        public void If_set_as_inactive_fails_then_redirects_to_details_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var userProfileController = new UserProfileController(profileService);
            userProfileController.UserName = "test";

            using (_mock.Record())
            {
                Expect.Call(profileService.SetAsInactiveByName("test")).Return(false);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect = null;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)userProfileController.Delete();
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details");
            #endregion
        }
				
    }


    [TestFixture]
    class Edit : BaseTest
    {
        [Test]
        public void Gets_profile_of_current_user_then_return_default_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var userProfileController = new UserProfileController(profileService);
            userProfileController.UserName = "test";

            using (_mock.Record())
            {
                Expect.Call(profileService.GetByName("test")).Return(_profile);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)userProfileController.Edit();
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model,Is.EqualTo(_profile));
            #endregion
        }

        [Test]
        public void Post_updates_profile_then_redirects_to_details_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var userProfileController = new UserProfileController(profileService);
            userProfileController.UserName = "test";

            userProfileController.ControllerContext = TestHelper.MockControllerContext(userProfileController);
            userProfileController.ValueProvider = new FormCollection().ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(profileService.GetByName("test")).Return(_profile);
                Expect.Call(profileService.UpdateProfile(_profile)).Return(true);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect = null;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)userProfileController.Edit(null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id",1);
            #endregion
        }


        [Test]
        public void Post_if_model_update_fails_then_adds_errormessage_and_returns_default_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var userProfileController = new UserProfileController(profileService);
            userProfileController.UserName = "test";

            userProfileController.ControllerContext = TestHelper.MockControllerContext(userProfileController);
            
            //Values that will fail
            userProfileController.ValueProvider = new FormCollection()
                {
                    {"Email",""}
                }
                .ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(profileService.GetByName("test")).Return(_profile);
                Expect.Call(profileService.UpdateProfile(_profile)).Repeat.Never();
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)userProfileController.Edit(null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(view.ViewData["Error"], Is.EqualTo("Validation Error"));
            #endregion
        }


        [Test]
        public void If_update_to_db_fails_return_default_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var userProfileController = new UserProfileController(profileService);
            userProfileController.UserName = "test";

            userProfileController.ControllerContext = TestHelper.MockControllerContext(userProfileController);
            userProfileController.ValueProvider = new FormCollection().ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(profileService.GetByName("test")).Return(_profile);
                Expect.Call(profileService.UpdateProfile(_profile)).Return(false);
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)userProfileController.Edit(null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(view.ViewData["Error"], Is.EqualTo("Problem Updating Profile"));

            #endregion
        }
				

    }
}
