using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Models;

namespace NHibernateTests
{
    [TestFixture]
    public class NHibernateInit
    {

        [SetUp]
        public void ResetSchema()
        {
            NHiberanteDal.SessionFactory.ResetSchema();
        }

        [Test]
        public void FillData()
        {
            //CreateData
            GroupTypeModel sampleGroupType = new GroupTypeModel() { TypeName = "Testowy typ grupy" };
            NHiberanteDal.Repository<GroupTypeModel>.Add(sampleGroupType);
            GroupModel sampleGroup = new GroupModel() { GroupName = "Testowa grupa", GroupType = sampleGroupType };
            NHiberanteDal.Repository<GroupModel>.Add(sampleGroup);
            ForumModel sampleForum = new ForumModel() { Name = "Testowe forum", Author = "Testowy User" };
            NHiberanteDal.Repository<ForumModel>.Add(sampleForum);

            CourseTypeModel sampleCourseType = new CourseTypeModel() { TypeName = "Testowy typ kursu" };
            NHiberanteDal.Repository<CourseTypeModel>.Add(sampleCourseType);

            CourseModel samplecourse = new CourseModel()
            {
                Name = "Testowy kurs",
                CourseType = sampleCourseType,
                CreationDate = DateTime.Now,
                Group = sampleGroup,
                Forum = sampleForum
            };
            //FillData
            NHiberanteDal.Repository<CourseModel>.Add(samplecourse); 
        }

    }
}
