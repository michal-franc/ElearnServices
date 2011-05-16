using NUnit.Framework;
using Rhino.Mocks;
using elearn.CourseService;
using elearn.Controllers;
using NHiberanteDal.DTO;
using System;
using System.Web.Mvc;
using elearn.JsonMessages;

namespace NHibernateTests.MVCTests.Controllers.ShoutBox
{

    public class BaseTest
    {
        protected  MockRepository Mock;
        protected  ICourseService CourseService;
        protected  ShoutBoxController ShoutBoxController;

        protected readonly ShoutBoxMessageModelDto SampleMessage;
        protected readonly ShoutBoxMessageModelDto[] SampleMessages;

        public BaseTest()
        {

            SampleMessage = new ShoutBoxMessageModelDto
                {
                    Author="test",
                    Message ="test",
                    ShoutBoxId = 1,
                    TimePosted = DateTime.Now
                };

            SampleMessages = new []
            {new ShoutBoxMessageModelDto{Author = "test",Message = "test",TimePosted = DateTime.Now} };
        }

        [SetUp]
        public void SetUp()
        {
            Mock = new MockRepository();
            CourseService = Mock.DynamicMock<ICourseService>();
            ShoutBoxController = new ShoutBoxController(CourseService);
        }
    }

    [TestFixture]
    public  class Add : BaseTest
    {
        [Test]
        public void Post_adds_message_to_shoutbox_then_return_succes_json_msg()
        {
            #region Arrange

            ShoutBoxController.ControllerContext =
                TestHelper.MockControllerContext(ShoutBoxController).WithAuthenticatedUser("test");
            using (Mock.Record())
            {
                Expect.Call(CourseService.AddShoutBoxMessage(SampleMessage)).IgnoreArguments().Return(1);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
             result =(JsonResult)ShoutBoxController.Add(1, "test");
            }

            #endregion

            #region Assert
            Assert.That(result.Data,Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.True);
            #endregion
        }

        [Test]
        public void Post_if_adds_message_fails_then_return_failed_json_msg()
        {
            #region Arrange
            ShoutBoxController.ControllerContext =
                TestHelper.MockControllerContext(ShoutBoxController).WithAuthenticatedUser("test");


            using (Mock.Record())
            {
                Expect.Call(CourseService.AddShoutBoxMessage(SampleMessage)).IgnoreArguments().Return(null);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
                result = (JsonResult)ShoutBoxController.Add(1, "test");
            }

            #endregion

            #region Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.False);
            #endregion
        }
				
    }

    [TestFixture]
    public class Get : BaseTest
    {
        //Add test for Data
        [Test]
        public void Post_gets_messages_then_returns_succes_json_msg_with_data()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(CourseService.GetLatestShoutBoxMessages(1,10)).Return(SampleMessages);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
                result = (JsonResult)ShoutBoxController.GetMessages(1);
            }

            #endregion

            #region Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.True);
            #endregion
        }

        //Add test for empty array
        [Test]
        public void Post_if_gets_fails_then_returns_failure_json_msg_with_empty_array()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(CourseService.GetLatestShoutBoxMessages(1,10)).Return(null);
            }
            #endregion

            #region Act

            JsonResult result;
            using (Mock.Playback())
            {
                result = (JsonResult)ShoutBoxController.GetMessages(1);
            }

            #endregion

            #region Assert
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(((ResponseMessage)result.Data).IsSuccess, Is.False);

            #endregion
        }
    }
}
