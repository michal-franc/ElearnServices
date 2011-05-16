using Rhino.Mocks;
using elearn.Controllers;
using elearn.GroupService;
using NUnit.Framework;
using NHiberanteDal.DTO;
using System.Collections.Generic;
using System.Web.Mvc;
using elearn.ProfileService;
using elearn.JsonMessages;

namespace NHibernateTests.MVCTests.Controllers.Group
{

    public class BaseTest
    {
        protected MockRepository Mock;
        protected IGroupService GroupService;
        protected IProfileService ProfileService;
        protected GroupController GroupController;

        protected GroupModelDto SampleGroup;
        protected ProfileModelDto SampleProfile;

        protected List<GroupModelDto> SampleGroupList;

        public BaseTest()
        {
            SampleGroup = new GroupModelDto{GroupName = "test"};
            SampleProfile = new ProfileModelDto{ID=1};
            SampleGroupList = new List<GroupModelDto>
                                  {
                                      new GroupModelDto(),
                                      new GroupModelDto()
                                  };


        }

        [SetUp]
        public void SetUp()
        {
            Mock = new MockRepository();
            GroupService = Mock.DynamicMock<IGroupService>();
            ProfileService = Mock.DynamicMock<IProfileService>();
            GroupController = new GroupController(GroupService, ProfileService);
        }
    }

    [TestFixture]
    public class Details : BaseTest
    {

        [Test]
        public void Get_gets_group_and_returns_details_view_with_model()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(GroupService.GetGroup(1)).Return(SampleGroup);
            }
            #endregion

            #region Act

            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)GroupController.Details(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model,Is.InstanceOf<GroupModelDto>());
            #endregion
        }
				
    }

    [TestFixture]
    public class Join : BaseTest
    {

        [Test]
        public void Post_adds_profile_to_group_then_returns_succes_msg()
        {
            #region Arrange

            GroupController.ControllerContext =
                TestHelper.MockControllerContext(GroupController).WithAuthenticatedUser("test");

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(SampleProfile);
                Expect.Call(GroupService.AddProfileToGroup(1,1)).Return(true);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
               result = (JsonResult)GroupController.Join(1);
            }

            #endregion

            #region Assert
            Assert.That(result.Data,Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.True);

            #endregion
        }

        [Test]
        public void Post_if_adds_profile_fails_then_return_error_msg()
        {
            #region Arrange

            GroupController.ControllerContext =
                    TestHelper.MockControllerContext(GroupController).WithAuthenticatedUser("test");


            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(SampleProfile);
                Expect.Call(GroupService.AddProfileToGroup(1, 1)).Return(false);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
                result = (JsonResult)GroupController.Join(1);
            }

            #endregion

            #region Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.False);
            #endregion
        }
				
    }

    [TestFixture]
    public class Leave : BaseTest
    {
        [Test]
        public void Post_removes_profile_from_group_then_returns_succes_msg()
        {
            #region Arrange

            GroupController.ControllerContext =
                TestHelper.MockControllerContext(GroupController).WithAuthenticatedUser("test");

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(SampleProfile);
                Expect.Call(GroupService.RemoveProfileFromGroup(1,1)).Return(true);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
                result = (JsonResult)GroupController.Leave(1);
            }

            #endregion

            #region Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.True);

            #endregion
        }

        [Test]
        public void Post_if_removing_profile_fails_then_return_error_msg()
        {
            #region Arrange

            GroupController.ControllerContext =
                TestHelper.MockControllerContext(GroupController).WithAuthenticatedUser("test");

            using (Mock.Record())
            {
                Expect.Call(ProfileService.GetByName("test")).Return(SampleProfile);
                Expect.Call(GroupService.RemoveProfileFromGroup(1, 1)).Return(false);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
                result = (JsonResult)GroupController.Leave(1);
            }

            #endregion

            #region Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.False);

            #endregion
        }
    }

}
