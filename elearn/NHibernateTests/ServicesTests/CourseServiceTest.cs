using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using ELearnServices;
using NHiberanteDal.DTO;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    class CourseServiceTest : InMemoryWithSampleData
    {
        [Test]
        public void Can_get_all_courses_signatures()
        {
            #region Arrange
            #endregion

            #region Act

            var courseSignatures = new CourseService().GetAllSignatures();

            #endregion

            #region Assert
            Assert.That(courseSignatures,Is.Not.Null);
            Assert.That(courseSignatures.Count, Is.EqualTo(3));
            Assert.That(courseSignatures.First(), Is.InstanceOf(typeof(CourseSignatureDto)));
            #endregion
        }

        [Test]
        public void Can_get_full_course_dto_with_latest_survey()
        {
            #region Arrange
            using (var session = DataAccess.OpenSession())
            {
                TestCourse1.Surveys.Add(TestSurvey);
                TestCourse1.Surveys.Add(TestLatestSurvey);
                session.SaveOrUpdate(TestCourse1);
                session.Flush();
            }
            #endregion

            #region Act

            var course = new CourseService().GetById(1);

            #endregion

            #region Assert
            Assert.That(course.ShoutBox,Is.Not.Null);
            Assert.That(course, Is.InstanceOf(typeof(CourseDto)));
            Assert.That(course.LatestSurvey,Is.Not.Null);
            Assert.That(course.LatestSurvey.DateCreated, Is.EqualTo(new DateTime(2011, 1, 1)));
            #endregion
        }

        [Test]
        public void Can_get_course_latest_test()
        {
            #region Arrange
            using (var session = DataAccess.OpenSession())
            {
                TestCourse1.Tests.Add(TestTest);
                TestCourse1.Tests.Add(LatestTest);
                session.SaveOrUpdate(TestCourse1);
                session.Flush();
            }
            #endregion

            #region Act

            TestDto test = new CourseService().GetLatestTest(1);

            #endregion

            #region Assert
            Assert.That(test, Is.Not.Null);
            Assert.That(test, Is.InstanceOf(typeof(TestDto)));
            Assert.That(test.CreationDate, Is.EqualTo(new DateTime(2011,1,1)));
            #endregion
        }

        [Test]
        public void Can_get_course_all_testssignatures()
        {
            #region Arrange
            using (var session = DataAccess.OpenSession())
            {
                TestCourse1.Tests.Add(TestTest);
                TestCourse1.Tests.Add(LatestTest);
                session.SaveOrUpdate(TestCourse1);
                session.Flush();
            }
            #endregion

            #region Act

            var tests = new CourseService().GetAllTestsSignatures(1);


            #endregion

            #region Assert
            Assert.That(tests, Is.Not.Null);
            Assert.That(tests.First(), Is.InstanceOf(typeof(TestSignatureDto)));
            Assert.That(tests.Count, Is.EqualTo(2));
            #endregion
        }

        [Test]
        public void Can_filter_courses_by_name()
        {
            #region Arrange
            #endregion

            #region Act

            List<CourseDto> filteredCourses = new CourseService().GetByName("test1");

            #endregion

            #region Assert
            Assert.That(filteredCourses.Count,Is.EqualTo(2));
            #endregion
        }

        [Test]
        public void Can_filter_courses_by_course_type()
        {
            #region Arrange
            #endregion

            #region Act

            List<CourseDto> filteredCourses = new CourseService().GetByCourseType(CourseTypeModelDto.Map(TestCourseType));


            #endregion

            #region Assert
            Assert.That(filteredCourses.Count, Is.EqualTo(1));
            #endregion
        }

        [Test]
        public void Can_add_new_course()
        {
            new CourseService();
            #region Arrange
            var newCourse = new CourseDto
                                {
                CreationDate = DateTime.Now,
                CourseType = CourseTypeModelDto.Map(TestCourseType),
                Forum = new ForumModelDto { Author = "test", Name = "added forum" },
                Group = new GroupModelDto
                            {
                    GroupName = "added test",
                    GroupType = GroupTypeModelDto.Map(TestGroupType)
                    },
                Logo = "test/jpg",
                Name = "test add",
                ShoutBox = new ShoutboxModelDto()
                                }; 
            #endregion

            #region Act

            int? id = new CourseService().AddCourse(newCourse);
            CourseDto testingAddedCourse=null;
            if (id.HasValue)
            {
                 testingAddedCourse = new CourseService().GetById(id.Value);
            }

            #endregion

            #region Assert
            Assert.That(testingAddedCourse,Is.Not.Null);
            if (testingAddedCourse != null)
            {
                Assert.That(testingAddedCourse.Name, Is.EqualTo("test add"));
                Assert.That(testingAddedCourse.Group.GroupName, Is.EqualTo("added test"));
                Assert.That(testingAddedCourse.Forum.Name, Is.EqualTo("added forum"));
            }

            #endregion
        }

        [Test]
        public void Can_update_course_property()
        {
            #region Arrange
            var course = new CourseService().GetById(1);
            course.Name = "changed name";
            #endregion

            #region Act

            var updateOk = new CourseService().Update(course);
            course = new CourseService().GetById(1);
            #endregion

            #region Assert
            Assert.That(updateOk, Is.True);
            Assert.That(course.Name,Is.EqualTo("changed name"));
            #endregion
        }

        [Test]
        public void Can_update_course_reference()
        {
            #region Arrange
            var course = new CourseService().GetById(1);
            course.CourseType = CourseTypeModelDto.Map(TestCourseType1);
            #endregion

            #region Act
            var updateOk  = new CourseService().Update(course);
            course = new CourseService().GetById(1);
            #endregion

            #region Assert
            Assert.That(updateOk,Is.True);
            Assert.That(course.CourseType.TypeName, Is.EqualTo("Matematyka"));
            #endregion
        }


        [Test]
        public void Can_remove_course_by_id()
        {
            #region Arrange

            int id = -1;
            DataAccess.InTransaction(session=>
            {
                id = (int)session.Save(TestCourse3);
            });

            #endregion

            #region Act

            bool ok = new CourseService().Remove(id);

            #endregion

            #region Assert
            Assert.IsTrue(ok);
            #endregion
        }

        [Test]
        public void Can_remove_with_nested_tests_by_id()
        {
            #region Arrange

            TestQuestion.Answers.Add(TestQuestionAnswer);
            TestTest.Questions.Add(TestQuestion);
            TestCourse3.Tests.Add(TestTest);

            int id = -1;
            DataAccess.InTransaction(session =>
            {
                id = (int)session.Save(TestCourse3);
            });

            #endregion

            #region Act

            bool ok = new CourseService().Remove(id);

            #endregion

            #region Assert
            Assert.IsTrue(ok);
            #endregion
        }


        [Test]
        public void Can_remove_with_nested_contents_by_id()
        {
            #region Arrange

            TestCourse3.Contents.Add(TestContent);

            int id = -1;
            DataAccess.InTransaction(session =>
            {
                id = (int)session.Save(TestCourse3);
            });

            #endregion

            #region Act

            bool ok = new CourseService().Remove(id);

            #endregion

            #region Assert
            Assert.IsTrue(ok);
            #endregion
        }

        [Test]
        public void Can_remove_with_nested_surveys_by_id()
        {
            #region Arrange

            TestCourse3.Surveys.Add(TestSurvey);

            int id = -1;
            DataAccess.InTransaction(session =>
            {
                id = (int)session.Save(TestCourse3);
            });

            #endregion

            #region Act

            bool ok = new CourseService().Remove(id);

            #endregion

            #region Assert
            Assert.IsTrue(ok);
            #endregion
        }
				

        [Test]
        public void Can_add_new_shoutbox_message()
        {
            #region Arrange

            var msg = new ShoutBoxMessageModelDto
            {Author = "test", Message = "test", TimePosted = DateTime.Now,ShoutBoxId = 1};
            #endregion

            #region Act

            int? id = new CourseService().AddShoutBoxMessage(msg);

            using (var session = DataAccess.OpenSession())
            {
                var shoutbox = session.Get<ShoutboxModel>(1);


                #endregion

                #region Assert

                Assert.IsNotNull(id);
                Assert.IsNotNull(shoutbox);
                Assert.That(shoutbox.Messages.First().Message, Is.EqualTo("test"));

                #endregion
            }
        }

        [Test]
        public void Can_get_shoutbox_messages()
        {
            #region Arrange

            #endregion

            #region Act

            var msgs = new CourseService().GetLatestShoutBoxMessages(1,10);


            #endregion

            #region Assert
            Assert.IsNotNull(msgs);
            Assert.That(msgs.Count,Is.EqualTo(2));
            #endregion
        }



        [Test]
        public void Can_get_last_message()
        {
            #region Arrange
            #endregion

            #region Act

            var msgs = new CourseService().GetLatestShoutBoxMessages(1,1);

            #endregion

            #region Assert
            Assert.That(msgs,Is.Not.Null);
            Assert.That(msgs.Count, Is.EqualTo(1));
            Assert.That(msgs.First().Message, Is.EqualTo("testLatest"));
            #endregion
        }
				
				
				
    }
}
