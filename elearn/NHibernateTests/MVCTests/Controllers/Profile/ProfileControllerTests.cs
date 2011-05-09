using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using elearn.Controllers;
using elearn;
using System.Web.Mvc;
using Ninject.Modules;
using NHiberanteDal.DTO;
using elearn.ProfileService;
using MvcContrib.TestHelper;
using System.Web;
using System.Web.Routing;

namespace NHibernateTests.MVCTests.Controllers.Profile
{

    class BaseTest
    {
        protected MockRepository _mock;
        protected IProfileService _profileService;
        protected ProfileController _profileController;
        protected ProfileModelDto _profile;
        protected ProfileModelDto[] _profileList;

        [SetUp]
        public  void SetUp()
        {
            _mock = new MockRepository();
            _profileService = _mock.DynamicMock<IProfileService>();
            _profileController = new ProfileController(_profileService);
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
        public void Redirects_to_lists()
        {
            #region Arrange
            var profileController = new ProfileController(_profileService);
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
            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
            }

            #endregion

            #region Act


            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Details(1);
            }
            #endregion

            #region Assert
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            Assert.IsEmpty(view.ViewName);
            #endregion
        }

        [Test]
        public void Get_if_wrong_profile_id_then_return_not_found_view()
        {
            #region Arrange
            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(null);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Details(1);
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
        public void Get_gets_allProfiles_then_returns_list_view()
        {
            #region Arrange
            using (_mock.Record())
            {
                Expect.Call(_profileService.GetAllProfiles()).Return(_profileList);
            }

            #endregion

            #region Act
            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.List();
            }
            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profileList));
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
            using (_mock.Record())
            {
                Expect.Call(_profileService.SetAsInactive(1)).Return(true);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect = null;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)_profileController.Delete(1);
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
            using (_mock.Record())
            {
                Expect.Call(_profileService.SetAsInactive(1)).Return(false);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect = null;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)_profileController.Delete(1);
            }
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            Assert.That(_profileController.ViewBag.Error, Is.Not.Null);
            Assert.That(_profileController.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Profile.SetAsInactiveFailed));
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
            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(null);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Edit(1);
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
            var roles = new string[] { "admin" };

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetAllRoles()).Return(roles);
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
            }
            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Edit(1);
            }
            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            Assert.That(view.ViewBag.Role, Is.InstanceOf(typeof(SelectList)));
            #endregion
        }

        [Test]
        public void Get_sets_profile_current_role_as_first_selected_and_dont_duplicate_roles()
        {
            #region Arrange
            var roles = new string[] { "user", "admin" , "basicuser" };

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetAllRoles()).Return(roles);
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
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

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetAllRoles()).Return(roles);
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            Assert.That(view.ViewBag.Role, Is.InstanceOf(typeof(SelectList)));
            Assert.That(((SelectList)view.ViewBag.Role).Count(), Is.EqualTo(1));
            Assert.That(((SelectList)view.ViewBag.Role).First().Text, Is.EqualTo(elearn.Common.Strings.NoRoleValue));

            #endregion
        }

        [Test]
        public void Post_updates_model_if_no_roles_selected_dont_update_roles_then_redirects_to_details_view()
        {
            #region Arrange
            _profileController.ControllerContext = TestHelper.MockControllerContext(_profileController).WithAuthenticatedUser("test");

            //Values that will simulate no role value
            _profileController.ValueProvider = new FormCollection()
                {
                    {"Role",elearn.Common.Strings.NoRoleValue}
                }
                .ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
                Expect.Call(_profileService.UpdateRole(_profile, true)).Repeat.Never();
                Expect.Call(_profileService.UpdateProfile(_profile)).Return(true);
            }

            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)_profileController.Edit(1, null);
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
            _profileController.ControllerContext = TestHelper.MockControllerContext(_profileController);
            _profileController.ValueProvider = new FormCollection().ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
                Expect.Call(_profileService.UpdateRole(_profile, true)).Return(true);
                Expect.Call(_profileService.UpdateProfile(_profile)).Return(true);
            }

            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)_profileController.Edit(1, null);
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
            _profileController.ControllerContext = TestHelper.MockControllerContext(_profileController);
            _profileController.ValueProvider = new FormCollection().ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
                Expect.Call(_profileService.UpdateRole(_profile, true)).Repeat.Never();
                Expect.Call(_profileService.UpdateProfile(_profile)).Return(false);
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Edit(1, null);
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
            _profileController.ControllerContext = TestHelper.MockControllerContext(_profileController);
            _profileController.ValueProvider = new FormCollection().ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
                Expect.Call(_profileService.UpdateRole(_profile, true)).Return(false);
                Expect.Call(_profileService.UpdateProfile(_profile)).Return(true);
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Edit(1, null);
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
            _profileController.ControllerContext = TestHelper.MockControllerContext(_profileController);
            
            //Values that will fail
            _profileController.ValueProvider = new FormCollection()
                {
                    {"Email",""}
                }
                .ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_profileService.GetProfile(1)).Return(_profile);
                Expect.Call(_profileService.UpdateRole(_profile, true)).Repeat.Never();
                Expect.Call(_profileService.UpdateProfile(_profile)).Repeat.Never();
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_profileController.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(ProfileModelDto)));
            #endregion
        }
    }
}
