using System;
using System.Collections.Generic;
using System.Linq;
using NHiberanteDal.Models;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;

namespace ELearnServices
{
    public class TestService : ITestService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public TestService()
        {
            DtoMappings.Initialize();
        }

        public int AddTest(int courseId,TestDto test)
        {
            try
            {
                int id = -1;
                DataAccess.InTransaction(session =>
                {
                    var course = session.Get<CourseModel>(courseId);
                    TestModel model = TestDto.UnMap(test);
                    id = (int)session.Save(model);
                    course.Tests.Add(model);
                    session.Save(course);

                });

                return id;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.AddTest - {0}", ex.Message);
                return -1;
            }

        }

        public void DeleteTest(TestDto test)
        {
            try
            {
                DataAccess.InTransaction(session => session.Delete(TestDto.UnMap(test)));
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.DeleteTest - {0}", ex.Message);
            }
        }

        public bool UpdateTest(TestDto test)
        {
            try
            {
                DataAccess.InTransaction(session => session.Update(TestDto.UnMap(test)));

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.UpdateTest - {0}", ex.Message);
                return false;
            }
        }

        public TestDto GetTestDetails(int id)
        {
            try
            {
                var testModel = new Repository<TestModel>().GetById(id);
                return TestDto.Map(testModel);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetTestDetails - {0}", ex.Message);
                return null;
            }
        }

        public List<TestSignatureDto> GetAllTests()
        {
            try
            {
                var tests = new Repository<TestModel>().GetAll().ToList();
                return TestSignatureDto.Map(tests);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetAllTests - {0}", ex.Message);
                return new List<TestSignatureDto>();
            }
        }

        public bool AddQuestion(int id, TestQuestionModelDto question)
        {
            try
            {
                DataAccess.InTransaction(session =>
                                             {
                                                 var test = session.Get<TestModel>(1);
                                                 test.Questions.Add(TestQuestionModelDto.UnMap(question));
                                                 session.Save(test);
                                             });

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.AddQuestion - {0}", ex.Message);
                return false;
            }
        }
    }
}
