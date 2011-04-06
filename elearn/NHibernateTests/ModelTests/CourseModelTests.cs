using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate;
using NHibernate.Linq;
using Models;
using NHiberanteDal.DataAccess;

namespace NHibernateTests.ModelTests
{
    [TestFixture]
    public class CourseModelTest : TestBase
    {

        [SetUp]
        public void AddSampleData()
        {

            //Test Group
            GroupTypeModel testGroupType = new GroupTypeModel() { TypeName="Test Group Type" };
            GroupModel testGroup = new GroupModel() { GroupName="Test Group", GroupType = testGroupType };

            DataAccess.InTransaction(session => session.Save(testGroupType));
            DataAccess.InTransaction(session => session.Save(testGroup));

            //Test Froum
            ForumModel testForum = new ForumModel() { Author="Test Author" , Name="Test Forum" };

            DataAccess.InTransaction(session => session.Save(testForum));

            //Test ShoutBox
            ShoutboxModel testShoutBox = new ShoutboxModel() {};

            DataAccess.InTransaction(session => session.Save(testShoutBox));

            //Test Course
            CourseTypeModel testCourseType = new CourseTypeModel() { TypeName = "Test Course Type" };
            CourseModel testCourse = new CourseModel() { 
                CourseType = testCourseType , CreationDate=DateTime.Now,
                 Forum = testForum , Group = testGroup , Name="Test Course" ,
                 ShoutBox = testShoutBox
            };

            DataAccess.InTransaction(session => session.Save(testCourseType));
            DataAccess.InTransaction(session => session.Save(testCourse));
        }

        [Test]
        public void CanAddCourse()
        {
            #region Arrange
            CourseModel testCourse = new CourseModel()
            {
                CreationDate = DateTime.Now,
                Name = "Test Course",
                Group = new GroupModel() { GroupName = "Add Test Group",GroupType = new GroupTypeModel() { TypeName="Add Test Group Type" }},
                ShoutBox = new ShoutboxModel(){},
                Forum = new ForumModel(){ Name="Add Test Group Forum", Author="Test Author"},
                CourseType = new CourseTypeModel(){TypeName = "Add Test Course Type"}
            };

            int beforeAddCount = 0;

            using (var session = DataAccess.OpenSession())
            {
                beforeAddCount = session.Linq<CourseModel>().Count();
            }
            #endregion

            #region Act

            Repository<CourseModel>.Add(testCourse);

            int afterAddCount = 0;

            using (var session = DataAccess.OpenSession())
            {
                afterAddCount = session.Linq<CourseModel>().Count();
            }

            #endregion

            #region Assert


            Assert.That(afterAddCount, Is.EqualTo(beforeAddCount + 1));
            
            #endregion
        }


        [Test]
        public void CanGetCourseByID()
        {
            #region Arrange
            #endregion

            #region Act
            CourseModel course = Repository<CourseModel>.GetByID(1);

	        #endregion

            #region Assert
            Assert.That(course, Is.Not.Null);
            Assert.That(course.Name, Contains.Substring("Test Course"));
            Assert.That(course.ID,Is.EqualTo(1));
            #endregion
        }


        [Test]
        public void GettingWrongIDReturnsNull()
        {
            #region Arrange
            #endregion

            #region Act
            CourseModel course = Repository<CourseModel>.GetByID(-1);
            #endregion

            #region Assert
            Assert.That(course, Is.Null);
            #endregion
        }



        [Test]
        public void CanGetCourseGroup()
        {
            #region Arrange
            #endregion

            #region Act

            CourseModel course = Repository<CourseModel>.GetByID(1);
            GroupModel group = course.Group;

            #endregion

            #region Assert
            #endregion
        }
				

        [Test]
        public void CanGetCourseForum()
        {
            Assert.Fail();
        }

        [Test]
        public void CanAddContent()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetContent()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetSurvey()
        {
            Assert.Fail();
        }

        [Test]
        public void CanGetTest()
        {
            Assert.Fail();
        }
    }
}
