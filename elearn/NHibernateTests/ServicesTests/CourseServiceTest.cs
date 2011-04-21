﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using ELearnServices;
using NHiberanteDal.DTO;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    class CourseServiceTest : InMemoryTest
    {
        CourseTypeModel _testCourseType;
        CourseTypeModel _testCourseType1;
        GroupTypeModel _testGroupType;
        GroupModel _testGroup;
        ForumModel _testForum;
        ShoutboxModel _testShoutBox;
        SurveyModel _testSurvey;
        SurveyModel _testLatestSurvey;
        CourseModel _testCourse1;
        CourseModel _testCourse2;

        [SetUp]
        public void SetUp()
        {
         _testCourseType = new CourseTypeModel() { TypeName="Fizyka" };
         _testCourseType1 = new CourseTypeModel() { TypeName = "Matematyka" };
         _testGroupType = new GroupTypeModel() { TypeName = "test" };
         _testGroup = new GroupModel() { GroupType = _testGroupType, GroupName = "test" };
         _testForum = new ForumModel() { Author = "test", Name = "test" };
         _testShoutBox = new ShoutboxModel() { };
         _testSurvey = new SurveyModel() { SurveyText = "Smiga chodzi fruwa ?", DateCreated = new DateTime(2010,1,1)};
         _testLatestSurvey = new SurveyModel() { SurveyText = "Smiga chodzi fruwa ?", DateCreated = new DateTime(2011, 1, 1) };
         _testCourse1 = new CourseModel()
        {
            CourseType = _testCourseType,
            ShoutBox = _testShoutBox,
            Forum = _testForum,
            Group = _testGroup,
            CreationDate = DateTime.Now,
            Description= "test",
            Logo="/test.jpg",
            Name="test", 
            Surveys = new List<SurveyModel>()
         
        };

         _testCourse2 = new CourseModel()
        {
            CourseType = _testCourseType1,
            ShoutBox = _testShoutBox,
            Forum = _testForum,
            Group = _testGroup,
            CreationDate = DateTime.Now,
            Description = "test1",
            Logo = "/test1.jpg",
            Name = "test1",

        };

            using (var session = DataAccess.OpenSession())
            {
                session.Save(_testCourseType);
                session.Save(_testGroupType);
                session.Save(_testGroup);
                session.Save(_testForum);
                session.Save(_testShoutBox);
                session.Save(_testCourseType1);
                session.Save(_testSurvey);
                session.Save(_testLatestSurvey);
            }
        }

        [Test]
        public void Can_get_all_courses_signatures()
        {
            #region Arrange
            using (var session = DataAccess.OpenSession())
            {
                session.Save(_testCourse1);
                session.Save(_testCourse2);
            }
            #endregion

            #region Act

            var courseSignatures = new CourseService().GetAllSignatures();

            #endregion

            #region Assert
            Assert.That(courseSignatures,Is.Not.Null);
            Assert.That(courseSignatures.Count, Is.EqualTo(2));
            Assert.That(courseSignatures.First(), Is.InstanceOf(typeof(CourseSignatureDto)));
            #endregion
        }

        [Test]
        public void Can_get_full_course_dto_with_latest_survey()
        {
            #region Arrange
            using (var session = DataAccess.OpenSession())
            {
                _testCourse1.Surveys.Add(_testSurvey);
                session.Save(_testCourse1);
                session.Flush();
                _testCourse1.Surveys.Add(_testLatestSurvey);
                session.Save(_testCourse1);
                session.Flush();
            }
            #endregion

            #region Act

            var course = new CourseService().GetById(1);

            #endregion

            #region Assert
            Assert.That(course.ShoutBox,Is.Not.Null);
            Assert.That(course.LatestSurvey,Is.Not.Null);
            Assert.That(course.LatestSurvey.DateCreated, Is.EqualTo(new DateTime(2011, 1, 1)));
            #endregion
        }

        [Test]
        public void Can_get_course_latest_Test()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_get_course_all_Tests()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_filter_courses_by_name()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_filter_courses_by_course_type()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }


        [Test]
        public void Can_add_new_course()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_add_new_survey_for_course()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }


        [Test]
        public void Can_add_new_test_for_course()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_update_course()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_update_survey()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }
				
    }
}
