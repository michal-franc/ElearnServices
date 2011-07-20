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
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public CourseService()
        {
            Logger.Info("Created CourseService");
            DtoMappings.Initialize();
        }

        public IList<CourseDto> GetAll()
        {
            try
            {
                return CourseDto.Map(new Repository<CourseModel>().GetAll().ToList());
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetAll - {0}",ex.Message);
                return new List<CourseDto>();
            }
        }

        public IList<CourseSignatureDto> GetAllSignatures()
        {
            try
            {
                return CourseSignatureDto.Map(new Repository<CourseModel>().GetAll().ToList());
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetAllSignatures - {0}", ex.Message);
                return new List<CourseSignatureDto>();
            }
        }

        public IList<CourseSignatureDto> GetCourseSignaturesByProfileId(int profileId)
        {
            try
            {
                using (var session = DataAccess.OpenSession())
                {
                    var courses = session.CreateCriteria(typeof(CourseModel)).List<CourseModel>().Where(c => c.Group.Users.Any(p => p.ID == profileId)).ToList();
                    return CourseSignatureDto.Map(courses);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetCourseSignaturesByProfileId - {0}", ex.Message);
                return new List<CourseSignatureDto>();
            }
        }

        public CourseDto GetById(int id)
        {
            try
            {
                CourseDto course;
                using (var session = DataAccess.OpenSession())
                {
                    course = CourseDto.Map(session.Get<CourseModel>(id));
                }
                return course;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetById - {0}", ex.Message);
                return null;
            }
        }

        public TestDto GetLatestTest(int id)
        {
            try
            {
                TestDto test;
                using (var session = DataAccess.OpenSession())
                {
                    test = TestDto.Map(
                        session.Get<CourseModel>(id)
                        .Tests.OrderByDescending(c => c.CreationDate).FirstOrDefault()
                        );
                }
                return test;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetLatestTest - {0}", ex.Message);
                return null;
            }
        }

        public IList<TestSignatureDto> GetAllTestsSignatures(int id)
        {
            try
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
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetAllTestsSignatures - {0}", ex.Message);
                return new List<TestSignatureDto>();
            }
        }

        public List<CourseDto> GetByName(string value)
        {
            try
            {
                List<CourseDto> returnedList;
                using (var session = DataAccess.OpenSession())
                {
                    returnedList = CourseDto.Map((List<CourseModel>)session.CreateQuery(new QueryCourseByName(value).Query).List<CourseModel>());
                }
                return returnedList;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetByName - {0}", ex.Message);
                return new List<CourseDto>();
            }
        }

        public List<CourseDto> GetByCourseType(CourseTypeModelDto testCourseType)
        {
            try
            {
                List<CourseDto> returnedList;
                using (var session = DataAccess.OpenSession())
                {
                    returnedList = CourseDto.Map((List<CourseModel>)session.CreateQuery(new QueryCourseByCourseType(CourseTypeModelDto.UnMap(testCourseType)).Query).List<CourseModel>());
                }
                return returnedList;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetByCourseType - {0}", ex.Message);
                return new List<CourseDto>();
            }
        }

        //todo poprawic updatowanie learning materialsow bo teraz jest burdel :X 2 azy sciagam niepotrzebnie encje
        public bool Update(CourseDto updatedCourse,bool reupload)
        {
            try
            {
                using (var session = DataAccess.OpenSession())
                {
                    if (reupload)
                    {
                        var originalCourse = session.Load<CourseModel>(updatedCourse.ID);

                        var course = CourseDto.UnMap(updatedCourse);
                        originalCourse.CourseType = session.Load<CourseTypeModel>(updatedCourse.CourseTypeId);
                        originalCourse.Description = course.Description;
                        originalCourse.Logo = course.Logo;
                        originalCourse.Name = course.Name;
                        originalCourse.News = course.News;
                        originalCourse.Password = course.Password;
                        originalCourse.ShortDescription = course.ShortDescription;
                        session.Update(originalCourse);
                        session.Flush();
                        return true;
                    }
                    else
                    {
                        session.Update(CourseDto.UnMap(updatedCourse));
                        session.Flush();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.Update - {0}", ex.Message);
                return false;
            }
        }

        public int? AddCourse(CourseDto newCourse)
        {
            try
            {
                var course = CourseDto.UnMap(newCourse);
                return new Repository<CourseModel>().Add(course);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.AddCourse - {0}", ex.Message);
                return null;
            }
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
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.Remove - {0}", ex.Message);
                return false;
            }
        }


        public List<CourseTypeModelDto> GetAllCourseTypes()
        {
            try
            {
                return CourseTypeModelDto.Map(new Repository<CourseTypeModel>().GetAll().ToList());
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetAllCourseTypes - {0}", ex.Message);
                return new List<CourseTypeModelDto>();
            }
        }

        public int? AddShoutBoxMessage(ShoutBoxMessageModelDto msg)
        {
            try
            {
                return new Repository<ShoutBoxMessageModel>().Add(ShoutBoxMessageModelDto.UnMap(msg));
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.AddShoutBoxMessage - {0}", ex.Message);
                return null;
            }
        }

        public IList<ShoutBoxMessageModelDto> GetLatestShoutBoxMessages(int shoutBoxId,int numberOfMessages)
        {
            try
            {
                List<ShoutBoxMessageModel> msgs;
                using (var session = DataAccess.OpenSession())
                {
                    msgs = session.Get<ShoutboxModel>(shoutBoxId).Messages.OrderByDescending(c => c.TimePosted).Take(numberOfMessages).ToList();
                }
                return ShoutBoxMessageModelDto.Map(msgs);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetLatestShoutBoxMessages - {0}", ex.Message);
                return new List<ShoutBoxMessageModelDto>();
            }
        }

        public bool CheckPassword(int courseId, string password)
        {
            try
            {
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
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.CheckPassword - {0}", ex.Message);
                return false;
            }
        }

        public List<CourseDto> GetByProfileId(int profileId)
        {
            try
            {
                using (var session = DataAccess.OpenSession())
                {
                    var courses = session.CreateCriteria(typeof(CourseModel)).List<CourseModel>().Where(c => c.Group.Users.Any(p => p.ID == profileId)).ToList();
                    return CourseDto.Map(courses);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetByProfileId - {0}", ex.Message);
                return new List<CourseDto>();
            }
        }

    }
}
