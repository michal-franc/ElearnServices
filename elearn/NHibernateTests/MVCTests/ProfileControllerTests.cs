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

namespace NHibernateTests.MVCTests
{
    [TestFixture]
    class ProfileControllerTests
    {
        MockRepository mock = new MockRepository();
        [Test]
        public void Index_Redirects_to_Detail_Action_with_id_of_current_user()
        {
            #region Arrange

            var profileModelDto = new ProfileModelDto(){ ID=1, Email="test@test.com", Name="test" };
            var profileService =(IProfileService)mock.DynamicMock(typeof(IProfileService));
            var profileController = new ProfileController(profileService);
            profileController.UserName = "test";
            Expect.Call(profileService.GetByName("test")).Return(profileModelDto);
            #endregion

            #region Act
            mock.ReplayAll();
            var redirect = (RedirectToRouteResult)profileController.Index();

            #endregion

            #region Assert
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Details"));
            Assert.That(redirect.RouteValues["id"], Is.EqualTo(1));
            mock.VerifyAll();
            #endregion
        }
    }
}
