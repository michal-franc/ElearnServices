using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.Models;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TestService" in code, svc and config file together.
    public class TestService : ITestService
    {
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
            DataAccess.InTransaction(session =>
            {
                session.Delete(TestDto.UnMap(test));
            });
        }

        public bool UpdateTest(TestDto test)
        {
            DataAccess.InTransaction(session =>
            {
                session.Update(TestDto.UnMap(test));
            });

            return true;
        }
    }
}
