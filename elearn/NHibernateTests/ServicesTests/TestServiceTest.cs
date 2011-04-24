using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ELearnServices;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    public class TestServiceTest : InMemoryWithSampleData
    {

        [Test]
        public void Can_Add_Test()
        {
            #region Arrange
            var test = new TestDto() { Author= ProfileModelDto.Map(_testPofile), CreationDate=DateTime.Now, Name="new test" , TestType= TestTypeModelDto.Map(_testTestType)};
            #endregion

            #region Act
            new TestService().AddTest(1, test);
            var tests= new CourseService().GetAllTestsSignatures(1);
            #endregion

            #region Assert
            Assert.That(tests.Count,Is.EqualTo(1));
            Assert.That(tests.First().Name, Is.EqualTo("new test"));
            #endregion
        }


        [Test]
        public void Can_delete_Test()
        {
            #region Arrange         
            var test = new TestDto() { Author = ProfileModelDto.Map(_testPofile), CreationDate = DateTime.Now, Name = "new test", TestType = TestTypeModelDto.Map(_testTestType) };
            test.ID =  new TestService().AddTest(1, test);
            var tests = new CourseService().GetAllTestsSignatures(1);
            Assert.That(tests.Count, Is.EqualTo(1));
            #endregion

            #region Act

            new TestService().DeleteTest(test);
             tests = new CourseService().GetAllTestsSignatures(1);

            #endregion

            #region Assert
            Assert.That(tests.Count, Is.EqualTo(0));
            #endregion
        }

        [Test]
        public void Can_Update_Test()
        {
            #region Arrange
            var test = new TestDto() { Author = ProfileModelDto.Map(_testPofile), CreationDate = DateTime.Now, Name = "new test", TestType = TestTypeModelDto.Map(_testTestType) };
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
        public void Can_Add_Test_Question()
        {
            #region Arrange
            TestQuestionModelDto question = new TestQuestionModelDto() 
            { 
                QuestionText = "test question", 
                Answers = new List<TestQuestionAnswerDto>()
                {new TestQuestionAnswerDto(){ Correct=false, NumberSelected=0, Text="test answer"} } };
            #endregion

            #region Act

            bool addedQuestion = new TestService().AddQuestion(1,question);

            #endregion

            #region Assert
            Assert.That(addedQuestion,Is.True);
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
        public void Can_Get_Test_Details()
        {
            #region Arrange
            var test = new TestDto()
            {
                Author = ProfileModelDto.Map(_testPofile),
                CreationDate = DateTime.Now,
                Name = "new test",
                TestType = TestTypeModelDto.Map(_testTestType),
                Questions =new List<TestQuestionModelDto>(){ TestQuestionModelDto.Map(_testQuestion)}
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
				

    }
}
