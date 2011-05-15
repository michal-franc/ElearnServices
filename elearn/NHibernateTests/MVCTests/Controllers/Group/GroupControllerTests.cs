using Rhino.Mocks;
using elearn.Controllers;
using elearn.GroupService;
using NUnit.Framework;
using NHiberanteDal.DTO;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NHibernateTests.MVCTests.Controllers.Group
{

    public class BaseTest
    {
        protected MockRepository Mock;
        protected IGroupService GroupService;
        protected GroupController GroupController;

        protected GroupModelDto SampleGroup;

        protected List<GroupModelDto> SampleGroupList;

        public BaseTest()
        {
                SampleGroup = new GroupModelDto{GroupName = "test"};
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
            GroupController = new GroupController(GroupService);
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

}
