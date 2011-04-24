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
        public void Can_get_TestTypes()
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
        public void Can_Add_Test_Question()
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
        public void Can_add_test_question_answers()
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
        public void Can_Get_Test_Details()
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
