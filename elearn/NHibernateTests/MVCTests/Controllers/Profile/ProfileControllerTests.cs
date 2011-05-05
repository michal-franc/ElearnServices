using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Rhino.Mocks;
using elearn.Controllers;
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
        protected ProfileModelDto _profile;
        protected ProfileModelDto[] _profileList;

        [SetUp]
        public  void SetUp()
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
        public void Redirect_to_lists()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);
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
        public void Get_profile_then_return_default_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);

            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
            }

            #endregion

            #region Act


            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Details(1);
            }
            #endregion

            #region Assert
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            Assert.IsEmpty(view.ViewName);
            #endregion
        }

        [Test]
        public void If_wrong_profile_id_then_return_not_found_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);

            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(null);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Details(1);
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
        public void Gets_allProfiles_then_returns_default_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);

            using (_mock.Record())
            {
                Expect.Call(profileService.GetAllProfiles()).Return(_profileList);
            }

            #endregion

            #region Act
            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.List();
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
        public void Sets_as_inactive_then_redirects_to_list_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var profileController = new ProfileController(profileService);

            using (_mock.Record())
            {
                Expect.Call(profileService.SetAsInactive(1)).Return(true);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect = null;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)profileController.Delete(1);
            }
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            #endregion
        }

        [Test]
        public void If_delete_profile_false_then_redirect_to_list_with_error()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var profileController = new ProfileController(profileService);

            using (_mock.Record())
            {
                Expect.Call(profileService.SetAsInactive(1)).Return(false);
            }

            #endregion

            #region Act

            RedirectToRouteResult redirect = null;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)profileController.Delete(1);
            }
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            Assert.That(profileController.ViewData["Error"], Is.EqualTo("Problem updating profile"));
            #endregion
        }
    }

    [TestFixture]
    class Edit : BaseTest
    {    
        [Test]
        public void If_wrong_id_then_returns_not_found_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);

            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(null);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsNull(view.ViewData.Model);
            Assert.That(view.ViewName, Is.EqualTo("NotFound"));
            #endregion
        }

        [Test]
        public void Gets_profile_and_roles_then_returns_default_view()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);
            var roles = new string[] { "admin" };

            using (_mock.Record())
            {
                Expect.Call(profileService.GetAllRoles()).Return(roles);
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            Assert.That(view.ViewData["Role"], Is.InstanceOf(typeof(SelectList)));


            #endregion
        }

        [Test]
        public void Sets_profile_current_role_as_first_selected_dont_duplicate_roles()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);
            var roles = new string[] { "admin" };

            using (_mock.Record())
            {
                Expect.Call(profileService.GetAllRoles()).Return(roles);
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            Assert.That(view.ViewData["Role"], Is.InstanceOf(typeof(SelectList)));

            //Check if first role in the list is the profile one
            Assert.That(((SelectList)view.ViewData["Role"]).First().Text, Is.EqualTo("admin"));

            //Check if there is only one admin
            Assert.That(((SelectList)view.ViewData["Role"]).Count(c => c.Text == "admin"), Is.EqualTo(1));
            #endregion
        }

        [Test]
        public void Gets_profile_and_if_no_roles_then_returns_default_view_with_roels_list_none()
        {
            #region Arrange
            var profileService = (IProfileService)_mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);
            var roles = new string[] {};

            using (_mock.Record())
            {
                Expect.Call(profileService.GetAllRoles()).Return(roles);
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
            }

            #endregion

            #region Act

            ViewResult view = null;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.EqualTo(_profile));
            Assert.That(view.ViewData["Role"], Is.InstanceOf(typeof(SelectList)));
            Assert.That(((SelectList)view.ViewData["Role"]).First().Text, Is.EqualTo("----"));

            #endregion
        }

        [Test]
        public void HttpPost_updates_model_and_if_no_roles_selected_dont_update_roles_then_redirects_to_details_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var profileController = new ProfileController(profileService);

            profileController.ControllerContext = TestHelper.MockControllerContext(profileController);


            //Values that will fail
            profileController.ValueProvider = new FormCollection()
                {
                    {"Role","----"}
                }
                .ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
                Expect.Call(profileService.UpdateRole(_profile, true)).Repeat.Never();
                Expect.Call(profileService.UpdateProfile(_profile)).Return(true);
            }

            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)profileController.Edit(1, null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id", 1);
            #endregion
        }
    
        [Test]
        public void HttpPost_updates_model_and_updates_role_then_redirects_to_details_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock <IProfileService>();
            var profileController = new ProfileController(profileService);

            profileController.ControllerContext = TestHelper.MockControllerContext(profileController);
            profileController.ValueProvider = new FormCollection().ToValueProvider();


            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
                Expect.Call(profileService.UpdateRole(_profile, true)).Return(true);
                Expect.Call(profileService.UpdateProfile(_profile)).Return(true);
            }

            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)profileController.Edit(1, null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id",1);
            #endregion
        }

        [Test]
        public void If_update_to_db_false_return_default_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var profileController = new ProfileController(profileService);

            profileController.ControllerContext = TestHelper.MockControllerContext(profileController);
            profileController.ValueProvider = new FormCollection().ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
                Expect.Call(profileService.UpdateRole(_profile, true)).Repeat.Never();
                Expect.Call(profileService.UpdateProfile(_profile)).Return(false);
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model,Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(view.ViewData["Error"], Is.EqualTo("Problem Updating Profile"));

            #endregion
        }


        [Test]
        public void If_update_role_fails_then_return_default_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var profileController = new ProfileController(profileService);

            profileController.ControllerContext = TestHelper.MockControllerContext(profileController);
            profileController.ValueProvider = new FormCollection().ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
                Expect.Call(profileService.UpdateRole(_profile, true)).Return(false);
                Expect.Call(profileService.UpdateProfile(_profile)).Return(true);
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(view.ViewData["Error"], Is.EqualTo("Problem Updating Role"));
            #endregion
        }


        [Test]
        public void If_model_update_false_dont_update_profile_role_then_return_default_view()
        {
            #region Arrange
            var profileService = _mock.DynamicMock<IProfileService>();
            var profileController = new ProfileController(profileService);

            profileController.ControllerContext = TestHelper.MockControllerContext(profileController);
            
            //Values that will fail
            profileController.ValueProvider = new FormCollection()
                {
                    {"Email",""}
                }
                .ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(profileService.GetProfile(1)).Return(_profile);
                Expect.Call(profileService.UpdateRole(_profile, true)).Repeat.Never();
                Expect.Call(profileService.UpdateProfile(_profile)).Repeat.Never();
            }

            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)profileController.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(ProfileModelDto)));
            Assert.That(view.ViewData["Error"], Is.EqualTo("Validation Error"));
            #endregion
        }
    }
}
