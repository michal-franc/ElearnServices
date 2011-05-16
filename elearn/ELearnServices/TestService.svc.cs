using System.Collections.Generic;
using System.Linq;
using NHiberanteDal.Models;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TestService" in code, svc and config file together.
    public class TestService : ITestService
    {
        public TestService()
        {
            DTOMappings.Initialize();
        }

        public int AddTest(int courseId,TestDto test)
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

        public void DeleteTest(TestDto test)
        {
            DataAccess.InTransaction(session => session.Delete(TestDto.UnMap(test)));
        }

        public bool UpdateTest(TestDto test)
        {
            DataAccess.InTransaction(session => session.Update(TestDto.UnMap(test)));

            return true;
        }

        public TestDto GetTestDetails(int id)
        {
            var testModel = new Repository<TestModel>().GetById(id);
            return TestDto.Map(testModel);
        }

        public List<TestSignatureDto> GetAllTests()
        {
            var tests = new Repository<TestModel>().GetAll().ToList();
            return TestSignatureDto.Map(tests);
        }

        public bool AddQuestion(int id, TestQuestionModelDto question)
        {
            DataAccess.InTransaction(session =>
            {
                var test = session.Get<TestModel>(1);
                test.Questions.Add(TestQuestionModelDto.UnMap(question));
                session.Save(test);
            });

            return true;
        }
    }
}
