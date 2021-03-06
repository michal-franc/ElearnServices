﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ELearnServices;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
     class TestsServiceTest : InMemoryWithSampleData
    {

        [Test]
        public void Can_add_test()
        {
            #region Arrange
            var test = new TestDto
             { Author= ProfileModelDto.Map(TestPofile), CreationDate=DateTime.Now, Name="new test" , TestType= TestTypeModelDto.Map(TestTestType)};
            #endregion

            #region Act
            new TestService().AddTestToCourse(1, test);
            //var tests= new CourseService().GetAllTestsSignatures(1);
            #endregion

            #region Assert
            //Assert.That(tests.Count,Is.EqualTo(1));
            //Assert.That(tests.First().Name, Is.EqualTo("new test"));
            #endregion
        }


        [Test]
        public void Can_delete_test()
        {
            #region Arrange         
            var test = new TestDto{ Author = ProfileModelDto.Map(TestPofile), CreationDate = DateTime.Now, Name = "new test", TestType = TestTypeModelDto.Map(TestTestType) };
            test.ID =  new TestService().AddTestToCourse(1, test);
            //var tests = new CourseService().GetAllTestsSignatures(1);
            //Assert.That(tests.Count, Is.EqualTo(1));
            #endregion

            #region Act

            new TestService().DeleteTest(1);
             //tests = new CourseService().GetAllTestsSignatures(1);

            #endregion

            #region Assert
            //Assert.That(tests.Count, Is.EqualTo(0));
            #endregion
        }

        [Test]
        public void Can_update_test()
        {
            #region Arrange
            var test = new TestDto { Author = ProfileModelDto.Map(TestPofile), CreationDate = DateTime.Now, Name = "new test", TestType = TestTypeModelDto.Map(TestTestType) };
            using (var session = DataAccess.OpenSession())
            {
                var course = session.Get<CourseModel>(1);
                course.Tests.Add(TestDto.UnMap(test));
                session.Flush();
            }

            using (var session = DataAccess.OpenSession())
            {
                var course = session.Get<CourseModel>(1);
                Assert.That(course.Tests[0].Name, Is.EqualTo("new test"));
                test.ID = course.Tests[0].ID;
            }
            #endregion

            #region Act
            test.Name = "updated test";
            bool updateOk = new TestService().UpdateTest(test);

            #endregion

            #region Assert
            using (var session = DataAccess.OpenSession())
            {
                var course = session.Get<CourseModel>(1);
                Assert.That(course.Tests[0].Name, Is.EqualTo("updated test"));
                Assert.That(updateOk,Is.True);
            }
            #endregion
        }


        [Test]
        public void Can_add_test_question()
        {
            #region Arrange
            var question = new TestQuestionModelDto 
            { 
                QuestionText = "test question", 
                Answers = new List<TestQuestionAnswerDto>
                {new TestQuestionAnswerDto{ Correct=false, NumberSelected=0, Text="test answer"} } };
            #endregion

            #region Act

            var addedQuestionID = new TestService().AddQuestion(1,question);

            #endregion

            #region Assert
            Assert.That(addedQuestionID, Is.GreaterThan(-1));
            using (var session = DataAccess.OpenSession())
            {
                var test = session.Get<TestModel>(1);
                Assert.That(test.Questions.Count,Is.EqualTo(1));
                Assert.That(test.Questions.First().QuestionText,Is.EqualTo("test question"));
                Assert.That(test.Questions.First().Answers.Count, Is.EqualTo(1));
                Assert.That(test.Questions.First().Answers.First().Text, Is.EqualTo("test answer"));
            }
            #endregion
        }



        [Test]
        public void Can_get_all_tests()
        {
            #region Arrange
            #endregion

            #region Act

            var tests = new TestService().GetAllTests();

            #endregion

            #region Assert
            Assert.That(tests.Count,Is.EqualTo(2));
            #endregion
        }
				

        [Test]
        public void Can_get_test_details()
        {
            #region Arrange
            var test = new TestDto
            {
                Author = ProfileModelDto.Map(TestPofile),
                CreationDate = DateTime.Now,
                Name = "new test",
                TestType = TestTypeModelDto.Map(TestTestType),
                Questions =new List<TestQuestionModelDto>{ TestQuestionModelDto.Map(TestQuestion)}
            };
            using (var session = DataAccess.OpenSession())
            {
                var course = session.Get<CourseModel>(1);
                course.Tests.Add(TestDto.UnMap(test));
                session.Flush();
            }
            #endregion

            #region Act

            test =new TestService().GetTestDetails(3);

            #endregion

            #region Assert
            Assert.That(test.Name, Is.EqualTo("new test"));
            Assert.That(test.Questions.Count,Is.EqualTo(1));
            Assert.That(test.Questions.First().Answers.Count, Is.EqualTo(1));
            #endregion
        }

        [Test]
        public void Can_get_test_types()
        {
            #region Arrange

            #endregion

            #region Act

            var testTypes = new TestService().GetAllTests();

            #endregion

            #region Assert
            Assert.That(testTypes.Count, Is.EqualTo(2));
            #endregion
        }


        [Test]
        public void Can_add_asnwers_to_question()
        {
            #region Arrange

            var answers = new List<TestQuestionAnswerDto> {
                new TestQuestionAnswerDto{Correct = false, Text = "test"},
                new TestQuestionAnswerDto{Correct = false, Text = "test"},
                new TestQuestionAnswerDto{Correct = false, Text = "test"},
                new TestQuestionAnswerDto{Correct = false, Text = "test"}

            };
            #endregion

            #region Act

            var ok = new TestService().AddAnswers(1, answers);

            int count = 0;
            using (var context = DataAccess.OpenSession())
            {
                count = context.Get<TestQuestionModel>(1).Answers.Count;
            }

            #endregion

            #region Assert
            Assert.IsTrue(ok);
            Assert.That(count,Is.EqualTo(4));
            #endregion
        }
				
				

    }
}
