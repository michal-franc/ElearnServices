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

        protected CourseDto _course = new CourseDto() { ID=1 };

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

    }

    [TestFixture]
    public class Delete : BaseTest
    {

    }

    [TestFixture]
    public class Edit : BaseTest
    {

    }

    [TestFixture]
    public class Details : BaseTest
    {
        
        [Test]
        public void Gets_coursedto_by_id_then_return_default_view()
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
        public void Gets_coursesdtosignatures_and_display_default_view()
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
        public void Can_page_coursesdtosignature_list()
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
