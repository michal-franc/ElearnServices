using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using elearn.Controllers;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using elearn.ProfileService;
using MvcContrib.TestHelper;

namespace elearnTests.MVCTests.Controllers.Profile
{

    class BaseTest
    {
        protected MockRepository Mock;
        protected IProfileService ProfileService;
        protected ProfileController ProfileController;
        protected ProfileModelDto Profile;
        protected ProfileModelDto[] ProfileList;

        [SetUp]
        public  void SetUp()
        {
            Mock = new MockRepository();
            ProfileService = Mock.DynamicMock<IProfileService>();
            ProfileController = new ProfileController(ProfileService);
            Profile = new ProfileModelDto { ID = 1, Email = "test@test.com", Role = "admin", LoginName = "testuser" };
            ProfileList = new[]
                {
                    new ProfileModelDto { ID=1},
                    new ProfileModelDto { ID=2}
                };

        }
    }

    [TestFixture]
    class Index : BaseTest
    {
        [Test]
        public void Redirects_to_lists()
        {
            #region Arrange
            var profileController = new ProfileController(ProfileService);
            #endregion

            #region Act
            var redirect = (RedirectToRouteResult)profileController.Index();

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            #endregion
        }
    }

    [TestFixture]
    class Details : BaseTest
    {
        [Test]
        public void Get_gets_profile_then_returns_details_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
            }

            #endregion

            #region Act


            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Details(1);
            }
            #endregion

            #region Assert
            Assert.That(view.ViewData.Model, Is.EqualTo(Profile));
            Assert.IsEmpty(view.ViewName);
            #endregion
        }

        [Test]
        public void Get_if_wrong_profile_id_then_return_not_found_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(null);
            }

            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Details(1);
            }

            #endregion

            #region Assert
            Assert.IsNull(view.ViewData.Model);
            Assert.That(view.ViewName, Is.EqualTo("NotFound"));
            #endregion
        }
    }

    [TestFixture]
    class List : BaseTest
    {
        [Test]
        public void Get_gets_all_profiles_then_returns_list_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetAllProfiles()).Return(ProfileList);
            }

            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.List();
            }
            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(ProfileList));
            #endregion
        }

    }

    [TestFixture]
    class Delete : BaseTest
    {
        [Test]
        public void Post_sets_profile_as_inactive_then_redirects_to_list_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(ProfileService.SetAsInactive(1)).Return(true);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)ProfileController.Delete(1);
            }
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            #endregion
        }

        [Test]
        public void Post_if_set_as_inactive_profile_fails_then_redirect_to_list_with_error()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(ProfileService.SetAsInactive(1)).Return(false);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)ProfileController.Delete(1);
            }
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            Assert.That(ProfileController.ViewBag.Error, Is.Not.Null);
            Assert.That(ProfileController.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Profile.SetAsInactiveFailed));
            #endregion
        }
    }

    [TestFixture]
    class Edit : BaseTest
    {    
        [Test]
        public void Get_if_wrong_id_then_return_not_found_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(null);
            }

            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsNull(view.ViewData.Model);
            Assert.That(view.ViewName, Is.EqualTo("NotFound"));
            #endregion
        }

        [Test]
        public void Get_gets_profile_and_roles_then_returns_default_view()
        {
            #region Arrange
            var roles = new[] { "admin" };

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetAllRoles()).Return(roles);
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
            }
            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Edit(1);
            }
            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(Profile));
            Assert.That(view.ViewBag.Role, Is.InstanceOf(typeof(SelectList)));
            #endregion
        }

        [Test]
        public void Get_sets_profile_current_role_as_first_selected_and_dont_duplicate_roles()
        {
            #region Arrange
            var roles = new[] { "user", "admin" , "basicuser" };

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetAllRoles()).Return(roles);
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
            }

            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(Profile));
            Assert.That(view.ViewBag.Role, Is.InstanceOf(typeof(SelectList)));

            //Check if first role in the list is the profile one
            Assert.That(((SelectList)view.ViewBag.Role).First().Text, Is.EqualTo("admin"));

            //Check if there is only one admin
            Assert.That(((SelectList)view.ViewBag.Role).Count(c => c.Text == "admin"), Is.EqualTo(1));
            #endregion
        }

        [Test]
        public void Get_gets_profile_and_if_no_roles_then_returns_default_view_with_1_none_role()
        {
            #region Arrange
            var roles = new string[] {};

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetAllRoles()).Return(roles);
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
            }

            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(Profile));
            Assert.That(view.ViewBag.Role, Is.InstanceOf(typeof(SelectList)));
            Assert.That(((SelectList)view.ViewBag.Role).Count(), Is.EqualTo(1));
            Assert.That(((SelectList)view.ViewBag.Role).First().Text, Is.EqualTo(elearn.Common.Strings.NoRoleValue));

            #endregion
        }

        [Test]
        public void Post_updates_model_if_no_roles_selected_dont_update_roles_then_redirects_to_details_view()
        {
            #region Arrange
            ProfileController.ControllerContext = TestHelper.MockControllerContext(ProfileController).WithAuthenticatedUser("test");

            //Values that will simulate no role value
            ProfileController.ValueProvider = new FormCollection
                                                  {
                    {"Role",elearn.Common.Strings.NoRoleValue}
                }
                .ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
                Expect.Call(ProfileService.UpdateRole(Profile, true)).Repeat.Never();
                Expect.Call(ProfileService.UpdateProfile(Profile)).Return(true);
            }

            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)ProfileController.Edit(1, null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id", 1);
            #endregion
        }
    
        [Test]
        public void Post_updates_model_and_updates_role_then_redirects_to_details_view()
        {
            #region Arrange
            ProfileController.ControllerContext = TestHelper.MockControllerContext(ProfileController);
            ProfileController.ValueProvider = new FormCollection().ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
                Expect.Call(ProfileService.UpdateRole(Profile, true)).Return(true);
                Expect.Call(ProfileService.UpdateProfile(Profile)).Return(true);
            }

            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)ProfileController.Edit(1, null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id",1);
            #endregion
        }

        [Test]
        public void Post_if_update_to_db_fails_then_return_edit_view_with_error()
        {
            #region Arrange
            ProfileController.ControllerContext = TestHelper.MockControllerContext(ProfileController);
            ProfileController.ValueProvider = new FormCollection().ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
                Expect.Call(ProfileService.UpdateRole(Profile, true)).Repeat.Never();
                Expect.Call(ProfileService.UpdateProfile(Profile)).Return(false);
            }

            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model,Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(view.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Profile.ProfileUpdateFail));

            #endregion
        }


        [Test]
        public void Post_if_update_role_fails_then_return_edit_view_with_error()
        {
            #region Arrange
            ProfileController.ControllerContext = TestHelper.MockControllerContext(ProfileController);
            ProfileController.ValueProvider = new FormCollection().ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
                Expect.Call(ProfileService.UpdateRole(Profile, true)).Return(false);
                Expect.Call(ProfileService.UpdateProfile(Profile)).Return(true);
            }

            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(view.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Profile.RoleUpdateFail));
            #endregion
        }


        [Test]
        public void If_model_update_false_dont_update_profile_role_then_return_default_view()
        {
            #region Arrange
            ProfileController.ControllerContext = TestHelper.MockControllerContext(ProfileController);
            
            //Values that will fail
            ProfileController.ValueProvider = new FormCollection
                                                  {
                    {"Email",""}
                }
                .ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetProfile(1)).Return(Profile);
                Expect.Call(ProfileService.UpdateRole(Profile, true)).Repeat.Never();
                Expect.Call(ProfileService.UpdateProfile(Profile)).Repeat.Never();
            }

            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)ProfileController.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(ProfileModelDto)));
            #endregion
        }
    }
}
