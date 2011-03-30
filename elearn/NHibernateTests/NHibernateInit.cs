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
            NHiberanteDal.Repository.SessionFactory.ResetSchema();
        }

        [Test]
        public void FillData()
        {
            //CreateData
            GroupTypeModel sampleGroupType = new GroupTypeModel() { TypeName = "Testowy typ grupy" };
            NHiberanteDal.Repository.Repository<GroupTypeModel> repo3 = new NHiberanteDal.Repository.Repository<GroupTypeModel>();
            repo3.Add(sampleGroupType);

            GroupModel sampleGroup = new GroupModel() { GroupName = "Testowa grupa", GroupType = sampleGroupType };

            NHiberanteDal.Repository.Repository<GroupModel> repo4 = new NHiberanteDal.Repository.Repository<GroupModel>();
            repo4.Add(sampleGroup);

            ForumModel sampleForum = new ForumModel() { Name = "Testowe forum", Author = "Testowy User" };

            NHiberanteDal.Repository.Repository<ForumModel> repo2= new NHiberanteDal.Repository.Repository<ForumModel>();
            repo2.Add(sampleForum);

            CourseTypeModel sampleCourseType = new CourseTypeModel() { TypeName = "Testowy typ kursu" };

            NHiberanteDal.Repository.Repository<CourseTypeModel> repo1 = new NHiberanteDal.Repository.Repository<CourseTypeModel>();
            repo1.Add(sampleCourseType);

            CourseModel samplecourse = new CourseModel()
            {
                Name = "Testowy kurs",
                CourseType = sampleCourseType,
                CreationDate = DateTime.Now,
                Group = sampleGroup,
                Forum = sampleForum
            };

            //FillData
            NHiberanteDal.Repository.Repository<CourseModel> repo = new NHiberanteDal.Repository.Repository<CourseModel>();
            repo.Add(samplecourse);

        }

    }
}
