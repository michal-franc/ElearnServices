using NUnit.Framework;
using elearn.Controllers;
using Rhino.Mocks;
using NHiberanteDal.DTO;
using elearn.ProfileService;
using System.Web.Mvc;
using MvcContrib.TestHelper;

namespace elearnTests.MVCTests.Controllers.UserProfile
{

    class BaseTest
    {
        protected MockRepository Mock;
        protected IProfileService ProfileService;
        protected UserProfileController UserProfileController;
        protected ProfileModelDto Profile;
        protected ProfileModelDto[] ProfileList;


        [SetUp]
        public void SetUp()
        {
            Mock = new MockRepository();

            ProfileService = (IProfileService)Mock.DynamicMock(typeof(IProfileService));
            UserProfileController = new UserProfileController(ProfileService);

            Profile = new ProfileModelDto { ID = 1, Email = "test@test.com", Role = "admin", Name = "testuser" };
            ProfileList = new[]
                {
                    new ProfileModelDto{ ID=1},
                    new ProfileModelDto{ ID=2}
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
            #endregion

            #region Act
            var   redirect = (RedirectToRouteResult)UserProfileController.Index();
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
        public void Gets_profile_by_name_then_returns_default_view()
        {
            #region Arrange
            UserProfileController.ControllerContext = TestHelper.MockControllerContext(UserProfileController)
                 .WithAuthenticatedUser("test");

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(Profile);
            }

            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)UserProfileController.Details();
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(Profile));
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
            UserProfileController.ControllerContext = TestHelper.MockControllerContext(UserProfileController)
                .WithAuthenticatedUser("test");
            using (Mock.Record())
            {
                Expect.Call(ProfileService.SetAsInactiveByName("test")).Return(true);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)UserProfileController.Delete();
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
            UserProfileController.ControllerContext = TestHelper.MockControllerContext(UserProfileController)
                .WithAuthenticatedUser("test");
            using (Mock.Record())
            {
                Expect.Call(ProfileService.SetAsInactiveByName("test")).Return(false);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)UserProfileController.Delete();
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
            UserProfileController.ControllerContext = TestHelper.MockControllerContext(UserProfileController)
                 .WithAuthenticatedUser("test");
            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(Profile);
            }

            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)UserProfileController.Edit();
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model,Is.EqualTo(Profile));
            #endregion
        }

        [Test]
        public void Post_updates_profile_then_redirects_to_details_view()
        {
            #region Arrange

            UserProfileController.ControllerContext = TestHelper.MockControllerContext(UserProfileController)
                .WithAuthenticatedUser("test");
            UserProfileController.ValueProvider = new FormCollection().ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(Profile);
                Expect.Call(ProfileService.UpdateProfile(Profile)).Return(true);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)UserProfileController.Edit(null);
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

            UserProfileController.ControllerContext = TestHelper.MockControllerContext(UserProfileController).WithAuthenticatedUser("test");

            //Values that will fail
            UserProfileController.ValueProvider = new FormCollection
                                                      {
                    {"Email",""}
                }
                .ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(Profile);
                Expect.Call(ProfileService.UpdateProfile(Profile)).Repeat.Never();
            }

            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)UserProfileController.Edit(null);
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

            UserProfileController.ControllerContext = TestHelper.MockControllerContext(UserProfileController).WithAuthenticatedUser("test");
            UserProfileController.ValueProvider = new FormCollection().ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(Profile);
                Expect.Call(ProfileService.UpdateProfile(Profile)).Return(false);
            }

            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)UserProfileController.Edit(null);
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
