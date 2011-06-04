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
            Logger.Info("Created TestService");
            DtoMappings.Initialize();
        }

        public int AddTest(int courseId,TestDto test)
        {
            try
            {
                var id = -1;
                DataAccess.InTransaction(session =>
                {
                    var course = session.Get<CourseModel>(courseId);
                    var model = TestDto.UnMap(test);
                    id = (int)session.Save(model);
                    course.Tests.Add(model);
                    session.Save(course);
                });
                Logger.Trace("Created Test id - {0}", id);
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

        public IList<TestTypeModelDto> GetTestTypes()
        {
            try
            {
                    var testTypes = new Repository<TestTypeModel>().GetAll().ToList();
                    return TestTypeModelDto.Map(testTypes);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CourseService.GetTestTypes - {0}", ex.Message);
                return new List<TestTypeModelDto>();
            }
        }

    }
}
 