using System;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using elearn.CourseService;
using elearn.Controllers;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using MvcContrib.TestHelper;

namespace NHibernateTests.MVCTests.Controllers.Course
{

    public class BaseTest
    {
        protected MockRepository Mock;
        protected ICourseService Service;
        protected CourseController Controller;


        // 13 courses
        protected readonly CourseSignatureDto[] CourseList;

        protected readonly CourseDto Course = new CourseDto { ID=1,Name="test" };
        protected readonly CourseDto ErrorCourse = new CourseDto { ID = 1, Name = String.Empty };

        protected BaseTest()
        {

            CourseList = new[]
                             {
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto(),
                                 new CourseSignatureDto()
                             };
        }


        [SetUp]
        public void SetUp()
        {
            Mock = new MockRepository();
            Service =  Mock.DynamicMock<ICourseService>();
            Controller = new CourseController(Service) {Limit = 10};
        }
    }


    [TestFixture]
    public class Index : BaseTest
    {
        [Test]
        public void Redirects_to_the_list()
        {
            #region Act
            var redirect = (RedirectToRouteResult)Controller.Index();
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List").WithParameter("id",1);
            #endregion
        }
				
    }

    [TestFixture]
    public class Create : BaseTest
    {
        [Test]
        public void Get_creates_empty_course_then_returns_create_view()
        {
            #region Act
            var view = (ViewResult)Controller.Create();

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.Not.Null);
            Assert.That(view.ViewData.Model,Is.InstanceOf(typeof(CourseDto)));
            #endregion
        }
				

        [Test]
        public void Post_adds_course_then_redirects_to_details_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(Service.AddCourse(Course)).IgnoreArguments().Return(1);
            }
            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)Controller.Create(new CourseDto());
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id",1);
            #endregion
        }


        [Test]
        public void Post_if_create_in_db_course_failes_then_return_error_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(Service.AddCourse(Course)).IgnoreArguments().Return(-1);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Create(null);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewData["Error"], Is.EqualTo(elearn.Common.ErrorMessages.Course.CourseAddToDbError));
            #endregion
        }

        [Test]
        public void Post_if_model_state_invalid_failes_then_return_error_view()
        {
            #region Arrange
            //Creating Error ModelState
            Controller.ModelState.Add("testError", new ModelState());
            Controller.ModelState.AddModelError("testError", "test");

            using (Mock.Record())
            {
                Expect.Call(Service.AddCourse(Course)).Repeat.Never();
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Create(null);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Course.CourseModelUpdateError));
            #endregion
        }
				
    }

    [TestFixture]
    public class Delete : BaseTest
    {
        [Test]
        public void Get_returns_delete_view()
        {
            #region Arrange

            #endregion

            #region Act
            var view  =(ViewResult)Controller.Delete();
            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            #endregion
        }


        [Test]
        public void Post_deletes_course_then_redirects_to_the_list_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(Service.Remove(1)).Return(true);
            }
            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)Controller.Delete(1, null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            #endregion
        }

        [Test]
        public void Post_if_delete_fails_redirects_to_the_error_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(Service.Remove(1)).Return(false);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Delete(1,null);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewData["Error"],Is.EqualTo("Problem Deleting Course"));
            #endregion
        }
    }

    [TestFixture]
    public class Edit : BaseTest
    {
        [Test]
        public void Returns_edit_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(Service.GetById(1)).Return(Course);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            #endregion
        }


        [Test]
        public void Post_updates_course_then_redirects_to_the_details_view()
        {
            #region Arrange
            Controller.ControllerContext = TestHelper.MockControllerContext(Controller);
            Controller.ValueProvider = TestHelper.ConvertEntityToFormCollection(Course).ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(Service.GetById(1)).Return(Course);
                Expect.Call(Service.Update(Course)).Return(true);
            }
            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)Controller.Edit(1,null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id",1);
            #endregion
        }

        [Test]
        public void Post_if_updates_fails_then_return_error_view()
        {
            #region Arrange
            Controller.ControllerContext = TestHelper.MockControllerContext(Controller);
            Controller.ValueProvider = TestHelper.ConvertEntityToFormCollection(Course).ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(Service.GetById(1)).Return(Course);
                Expect.Call(Service.Update(Course)).Return(false);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewData["Error"],Is.EqualTo("Problem Updating Course in DB"));
            #endregion
        }

        [Test]
        public void Post_if_validation_fails_then_return_edit_view()
        {
            #region Arrange
            Controller.ControllerContext = TestHelper.MockControllerContext(Controller);
            Controller.ValueProvider = TestHelper.ConvertEntityToFormCollection(ErrorCourse).ToValueProvider();

            using (Mock.Record())
            {
                Expect.Call(Service.GetById(1)).Return(ErrorCourse);
                Expect.Call(Service.Update(ErrorCourse)).Repeat.Never();
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Edit(1, null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData["Error"], Is.EqualTo("Problem Updating Course"));
            #endregion
        }
    }

    [TestFixture]
    public class Details : BaseTest
    {
        
        [Test]
        public void If_author_or_admin_gets_coursedto_by_id_then_return_details_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(Service.GetById(1)).Return(Course);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Details(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(CourseDto)));
            #endregion
        }


        [Test]
        public void If_not_author_and_not_admin_then_redirects_to_noacces_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(Service.GetById(1)).Return(Course);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.Details(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(CourseDto)));
            #endregion
        }
				
    }



    [TestFixture]
    public class List : BaseTest
    {
        [Test]
        public void Gets_courses_dto_signatures_and_returns_list_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(Service.GetAllSignatures()).Return(CourseList);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.List(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(CourseSignatureDto[])));
            #endregion
        }

        [Test]
        public void Can_page_courses_dto_signature_list()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(Service.GetAllSignatures()).Return(CourseList);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)Controller.List(2);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(CourseSignatureDto[])));
            Assert.That(((CourseSignatureDto[])view.ViewData.Model).Count(), Is.EqualTo(3));
            #endregion
        }
				
    }
}
