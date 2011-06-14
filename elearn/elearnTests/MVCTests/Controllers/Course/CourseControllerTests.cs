using System;
using System.Linq;
using elearn.ProfileService;
using elearn.CourseService;
using NUnit.Framework;
using Rhino.Mocks;
using elearn.Controllers;
using System.Web.Mvc;
using NHiberanteDal.DTO;
using MvcContrib.TestHelper;

namespace elearnTests.MVCTests.Controllers.Course
{
    //todotest fix tests
    public class BaseTest
    {
        protected MockRepository Mock;
        protected ICourseService CourseService;
        protected IProfileService ProfileService;
        protected CourseController CourseController;


        // 13 courses
        protected readonly CourseSignatureDto[] CourseList;
        protected readonly CourseTypeModelDto[] CourseTypesList;

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
            CourseTypesList = new[]
                                  {
                                      new CourseTypeModelDto(),
                                      new CourseTypeModelDto(),
                                      new CourseTypeModelDto()
                                  };

        }


        [SetUp]
        public void SetUp()
        {
            Mock = new MockRepository();
            CourseService =  Mock.DynamicMock<ICourseService>();
            CourseController = new CourseController(CourseService,ProfileService) {Limit = 10};
        }
    }


    [TestFixture]
    public class Index : BaseTest
    {
        [Test]
        public void Redirects_to_the_list()
        {
            #region Act
            var redirect = (RedirectToRouteResult)CourseController.Index();
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
        public void Get_creates_empty_course_and_gets_combobox_then_returns_create_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.GetAllCourseTypes()).Return(CourseTypesList);
            }

            #endregion

            #region Act

            ViewResult view;

            using (Mock.Playback())
            {
                view = (ViewResult) CourseController.Create();
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.IsNotNull(view.ViewBag.CourseTypes);
            Assert.That(view.ViewBag.CourseTypes, Is.InstanceOf(typeof(SelectList)));
            Assert.IsNotNull(view.ViewData.Model);
            Assert.That(view.ViewData.Model,Is.InstanceOf(typeof(CourseDto)));
            #endregion
        }
			

        [Test]
        public void Post_adds_course_then_redirects_to_details_action()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.AddCourse(Course)).IgnoreArguments().Return(1);
            }
            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)CourseController.Create(new CourseDto());
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
                Expect.Call(CourseService.AddCourse(Course)).IgnoreArguments().Return(null);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.Create(Course);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Course.AddToDbError));
            #endregion
        }

        [Test]
        public void Post_if_model_state_invalid_then_dont_add_course_and_return_error_view()
        {
            #region Arrange
            //Faking ModelState.IsValid = false
            CourseController.ModelState.Add("testError", new ModelState());
            CourseController.ModelState.AddModelError("testError", "test");

            using (Mock.Record())
            {
                Expect.Call(CourseService.AddCourse(Course)).Repeat.Never();
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.Create(Course);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Course.ModelUpdateError));
            #endregion
        }
				
    }

    [TestFixture]
    public class Delete : BaseTest
    {
        [Test]
        public void Get_returns_delete_view_with_id_as_a_model()
        {
            #region Act
            var view  =(ViewResult)CourseController.Delete(1);
            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model,Is.EqualTo(1));
            #endregion
        }


        [Test]
        public void Post_deletes_course_then_redirects_to_the_list_action()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.Remove(1)).Return(true);
            }
            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)CourseController.DeleteCourse(1);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("List");
            #endregion
        }

        [Test]
        public void Post_if_delete_fails_then_returns_error_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.Remove(1)).Return(false);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.DeleteCourse(1);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Course.DeleteError));
            #endregion
        }
    }

    [TestFixture]
    public class Edit : BaseTest
    {
        [Test]
        public void Get_gets_course_types_and_returns_edit_view_with_course_model()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.GetById(1)).Return(Course);
                Expect.Call(CourseService.GetAllCourseTypes()).Return(CourseTypesList);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.Edit(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.IsNotNull(view.ViewBag.CourseTypes);
            Assert.That(view.ViewBag.CourseTypes, Is.InstanceOf(typeof(SelectList)));
            Assert.IsNotNull(view.ViewData.Model);
            Assert.That(view.ViewData.Model,Is.InstanceOf(typeof(CourseDto)));
            #endregion
        }


        [Test]
        public void Post_updates_course_then_redirects_to_the_details_action()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(CourseService.Update(Course)).Return(true);
            }
            #endregion

            #region Act
            RedirectToRouteResult redirect;
            using (Mock.Playback())
            {
                redirect = (RedirectToRouteResult)CourseController.Edit(Course);
            }

            #endregion

            #region Assert
            redirect.AssertActionRedirect().ToAction("Details").WithParameter("id",1);
            #endregion
        }

        [Test]
        public void Post_if_update_to_db_fails_then_return_error_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(CourseService.Update(Course)).Return(false);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.Edit(Course);
            }

            #endregion

            #region Assert
            Assert.That(view.ViewName,Is.EqualTo("Error"));
            Assert.That(view.ViewBag.Error,Is.EqualTo(elearn.Common.ErrorMessages.Course.UpdateToDbError));
            #endregion
        }

        [Test] public void Post_if_model_state_false_then_return_edit_view()
        {
            #region Arrange

            //Faking ModelState.IsValid = false
            CourseController.ModelState.Add("testError", new ModelState());
            CourseController.ModelState.AddModelError("testError", "test");

            using (Mock.Record())
            {
                Expect.Call(CourseService.Update(ErrorCourse)).Repeat.Never();
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.Edit(Course);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewBag.Error, Is.EqualTo(elearn.Common.ErrorMessages.Course.ModelUpdateError));
            #endregion
        }
    }

    [TestFixture]
    public class Details : BaseTest
    {
        
        [Test]
        public void Get_if_author_or_admin_gets_coursedto_by_id_then_return_details_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.GetById(1)).Return(Course);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.Details(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(CourseDto)));
            #endregion
        }


        [Test]
        public void Get_if_not_author_and_not_admin_then_redirects_to_noacces_view()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.GetById(1)).Return(Course);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.Details(1);
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
        public void Get_gets_courses_dto_signatures_and_returns_list_view()
        {
            #region Arrange
            using (Mock.Record())
            {
                Expect.Call(CourseService.GetAllSignatures()).Return(CourseList);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.List(1);
            }

            #endregion

            #region Assert
            Assert.IsEmpty(view.ViewName);
            Assert.That(view.ViewData.Model, Is.InstanceOf(typeof(CourseSignatureDto[])));
            #endregion
        }

        [Test]
        public void Get_can_page_courses_dto_signature_list()
        {
            #region Arrange

            using (Mock.Record())
            {
                Expect.Call(CourseService.GetAllSignatures()).Return(CourseList);
            }
            #endregion

            #region Act
            ViewResult view;
            using (Mock.Playback())
            {
                view = (ViewResult)CourseController.List(2);
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
