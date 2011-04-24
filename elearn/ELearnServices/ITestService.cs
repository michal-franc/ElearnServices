using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITestService" in both code and config file together.
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        bool AddQuestion(int id, TestQuestionModelDto question);

        [OperationContract]
        TestDto GetTestDetails(int id);

        [OperationContract]
        bool UpdateTest(TestDto test);

        [OperationContract]
        void DeleteTest(TestDto test);

        [OperationContract]
        int AddTest(int courseId, TestDto test);
    }
}
