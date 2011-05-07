using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        protected MockRepository _mock;
        protected ICourseService _service;
        protected CourseController _controller;


        // 13 courses
        protected CourseSignatureDto[] _courseList = new CourseSignatureDto[]
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

        protected CourseDto _course = new CourseDto() { ID=1,Name="test" };
        protected CourseDto _errorCourse = new CourseDto() { ID = 1, Name = String.Empty };


        [SetUp]
        public void SetUp()
        {
            _mock = new MockRepository();
            _service =  _mock.DynamicMock<ICourseService>();
            _controller = new CourseController(_service);
            _controller.Limit = 10;
        }
    }


    [TestFixture]
    public class Index : BaseTest
    {

        [Test]
        public void Redirects_to_the_list()
        {
            #region Arrange

            #endregion

            #region Act
            RedirectToRouteResult redirect = (RedirectToRouteResult)_controller.Index();
            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List").WithParameter("id",1);
            #endregion
        }
				
    }

    [TestFixture]
    public class Add : BaseTest
    {
        [Test]
        public void Get_creates_empty_course_then_returns_add_view()
        {
            #region Arrange

            #endregion

            #region Act
            var view = (ViewResult)_controller.Add();

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            #endregion
        }
				

        [Test]
        public void Post_creates_new_course_then_redirects_to_details_view()
        {
            #region Arrange

            _controller.ControllerContext = TestHelper.MockControllerContext(_controller);
            _controller.ValueProvider = TestHelper.ConvertEntityToFormCollection(_course).ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_service.AddCourse(_course)).IgnoreArguments().Return(1);
            }
            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (_mock.Playback())
            {
                redirect = (RedirectToRouteResult)_controller.Add(null);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id",1);
            #endregion
        }


        [Test]
        public void Post_if_creates_new_course_failes_then_display_error_view_with_error_message()
        {
            #region Arrange
            _controller.ControllerContext = TestHelper.MockControllerContext(_controller);
            _controller.ValueProvider = TestHelper.ConvertEntityToFormCollection(_course).ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_service.AddCourse(_course)).IgnoreArguments().Return(-1);
            }
            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_controller.Add(null);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewData["Error"],Is.EqualTo("Problem in DB while creating Course"));
            #endregion
        }

        [Test]
        public void Post_if_update_of_model_failes_then_display_add_view_wit_error()
        {
            #region Arrange
            _controller.ControllerContext = TestHelper.MockControllerContext(_controller);
            _controller.ValueProvider = TestHelper.ConvertEntityToFormCollection(_errorCourse).ToValueProvider();

            using (_mock.Record())
            {
                Expect.Call(_service.AddCourse(_course)).Repeat.Never();
            }
            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_controller.Add(null);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData["Error"], Is.EqualTo("Update model error"));
            #endregion
        }
				
    }

    [TestFixture]
    public class Delete : BaseTest
    {
        [Test]
        public void Get_returns_delete_View()
        {
            #region Arrange

            using (_mock.Record())
            {
            }
            #endregion

            #region Act
            using (_mock.Playback())
            {
                Assert.Fail();
            }

            #endregion

            #region Assert
            #endregion
        }


        [Test]
        public void Post_deletes_course_then_redirects_to_the_list_view()
        {
            #region Arrange

            using (_mock.Record())
            {
            }
            #endregion

            #region Act
            using (_mock.Playback())
            {
                Assert.Fail();
            }

            #endregion

            #region Assert
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

            using (_mock.Record())
            {
            }
            #endregion

            #region Act
            using (_mock.Playback())
            {
                Assert.Fail();
            }

            #endregion

            #region Assert
            #endregion
        }


        [Test]
        public void Post_updates_course_then_redirects_to_the_list_view()
        {
            #region Arrange

            using (_mock.Record())
            {
            }
            #endregion

            #region Act
            using (_mock.Playback())
            {
                Assert.Fail();
            }

            #endregion

            #region Assert
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

            using (_mock.Record())
            {
                Expect.Call(_service.GetById(1)).Return(_course);
            }
            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_controller.Details(1);
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

            using (_mock.Record())
            {
                Expect.Call(_service.GetById(1)).Return(_course);
            }
            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_controller.Details(1);
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
            using (_mock.Record())
            {
                Expect.Call(_service.GetAllSignatures()).Return(_courseList);
            }
            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_controller.List(1);
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

            using (_mock.Record())
            {
                Expect.Call(_service.GetAllSignatures()).Return(_courseList);
            }
            #endregion

            #region Act
            ViewResult view;
            using (_mock.Playback())
            {
                view = (ViewResult)_controller.List(2);
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
