﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;

namespace NHibernateTests.ServicesTests
{
    class InMemoryWithSampleData : InMemoryTest
    {
        protected CourseTypeModel _testCourseType;
        protected CourseTypeModel _testCourseType1;
        protected GroupTypeModel _testGroupType;
        protected GroupModel _testGroup;
        protected ForumModel _testForum;
        protected ShoutboxModel _testShoutBox;
        protected SurveyModel _testSurvey;
        protected SurveyModel _testLatestSurvey;
        protected CourseModel _testCourse1;
        protected CourseModel _testCourse2;
        protected TestModel _testTest;
        protected TestModel _latestTest;
        protected TestTypeModel _testTestType;
        protected ProfileModel _testPofile;

        [SetUp]
        public void SetUp()
        {
            _testTestType = new TestTypeModel() { TypeName = "test" };
            _testPofile = new ProfileModel() { Name = "test" };
            _testTest = new TestModel() { Author = _testPofile, CreationDate = new DateTime(2010, 1, 1), Name = "test", TestType = _testTestType };
            _latestTest = new TestModel() { Author = _testPofile, CreationDate = new DateTime(2011, 1, 1), Name = "test", TestType = _testTestType };
            _testCourseType = new CourseTypeModel() { TypeName = "Fizyka" };
            _testCourseType1 = new CourseTypeModel() { TypeName = "Matematyka" };
            _testGroupType = new GroupTypeModel() { TypeName = "test" };
            _testGroup = new GroupModel() { GroupType = _testGroupType, GroupName = "test" };
            _testForum = new ForumModel() { Author = "test", Name = "test" };
            _testShoutBox = new ShoutboxModel() { };
            _testSurvey = new SurveyModel() { SurveyText = "Smiga chodzi fruwa ?", DateCreated = new DateTime(2010, 1, 1) };
            _testLatestSurvey = new SurveyModel() { SurveyText = "Smiga chodzi fruwa ?", DateCreated = new DateTime(2011, 1, 1) };
            _testCourse1 = new CourseModel()
            {
                CourseType = _testCourseType,
                ShoutBox = _testShoutBox,
                Forum = _testForum,
                Group = _testGroup,
                CreationDate = DateTime.Now,
                Description = "test",
                Logo = "/test.jpg",
                Name = "test",
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
                session.Save(_testPofile);
                session.Save(_testCourseType);
                session.Save(_testGroupType);
                session.Save(_testGroup);
                session.Save(_testForum);
                session.Save(_testShoutBox);
                session.Save(_testCourseType1);
                session.Save(_testSurvey);
                session.Save(_testLatestSurvey);
                session.Save(_testTestType);
                session.Save(_testTest);
                session.Save(_latestTest);
                session.Save(_testCourse1);
                session.Save(_testCourse2);
                session.Flush();
            }
        }

    }
}
