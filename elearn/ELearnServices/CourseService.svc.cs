using System;
using System.Collections.Generic;
using System.Linq;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess.QueryObjects;

namespace ELearnServices
{
    public class CourseService : ICourseService
    {
        public CourseService()
        {
            DtoMappings.Initialize();
        }

        public IList<CourseDto> GetAll()
        {
            return CourseDto.Map(new Repository<CourseModel>().GetAll().ToList());
        }

        public IList<CourseSignatureDto> GetAllSignatures()
        {
            return CourseSignatureDto.Map(new Repository<CourseModel>().GetAll().ToList());
        }

        public CourseDto GetById(int id)
        {
            CourseDto course;
            using (var session = DataAccess.OpenSession())
            {
               course=  CourseDto.Map(session.Get<CourseModel>(id));
            }
            return course;
        }

        public TestDto GetLatestTest(int id)
        {
            TestDto test;
            using(var session = DataAccess.OpenSession())
            {
                test = TestDto.Map(
                    session.Get<CourseModel>(id)
                    .Tests.OrderByDescending(c => c.CreationDate).FirstOrDefault()
                    );
            }
            return test;
        }

        public IList<TestSignatureDto> GetAllTestsSignatures(int id)
        {
            IList<TestSignatureDto> tests;
            using (var session = DataAccess.OpenSession())
            {
                tests = TestSignatureDto.Map(
                    session.Get<CourseModel>(id).Tests.ToList()
                    );
            }
            return tests;
        }

        public List<CourseDto> GetByName(string value)
        {
            List<CourseDto> returnedList;
            using (var session = DataAccess.OpenSession())
            {
                returnedList =CourseDto.Map((List<CourseModel>)session.CreateQuery(new QueryCourseByName(value).Query).List<CourseModel>());
            }
            return returnedList;
        }

        public List<CourseDto> GetByCourseType(CourseTypeModelDto testCourseType)
        {
            List<CourseDto> returnedList;
            using (var session = DataAccess.OpenSession())
            {
                returnedList = CourseDto.Map((List<CourseModel>)session.CreateQuery(new QueryCourseByCourseType(CourseTypeModelDto.UnMap(testCourseType)).Query).List<CourseModel>());
            }
            return returnedList;
        }

        public bool Update(CourseDto updatedCourse)
        {
            var course = CourseDto.UnMap(updatedCourse);
            return new Repository<CourseModel>().Update(course);
        }

        public int? AddCourse(CourseDto newCourse)
        {
            var course = CourseDto.UnMap(newCourse);
            return new Repository<CourseModel>().Add(course);
        }


        public bool Remove(int id)
        {
            try
            {
                using (var session = DataAccess.OpenSession())
                {
                    var course = session.Get<CourseModel>(id);
                    session.Delete(course);
                    session.Flush();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<CourseTypeModelDto> GetAllCourseTypes()
        {
            return CourseTypeModelDto.Map(new Repository<CourseTypeModel>().GetAll().ToList());
        }

        public int? AddShoutBoxMessage(ShoutBoxMessageModelDto msg)
        {
            return new Repository<ShoutBoxMessageModel>().Add(ShoutBoxMessageModelDto.UnMap(msg));
        }

        public IList<ShoutBoxMessageModelDto> GetLatestShoutBoxMessages(int shoutBoxId,int numberOfMessages)
        {
            List<ShoutBoxMessageModel> msgs;
            using (var session = DataAccess.OpenSession())
            {
                msgs = session.Get<ShoutboxModel>(shoutBoxId).Messages.OrderByDescending(c => c.TimePosted).Take(numberOfMessages).ToList();
            }
            return ShoutBoxMessageModelDto.Map(msgs);
        }

        public bool CheckPassword(int courseId, string password)
        {
            List<ShoutBoxMessageModel> msgs;
            string pass;
            using (var session = DataAccess.OpenSession())
            {
                pass = session.Get<CourseModel>(courseId).Password;
            }

            if (pass == null)
            {
                return true;
            }
            return password == pass;
        }
    }
}
