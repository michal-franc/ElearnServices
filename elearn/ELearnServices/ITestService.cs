using System.Collections.Generic;
using System.ServiceModel;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // : You can use the "Rename" command on the "Refactor" menu to change the interface name "ITestService" in both code and config file together.
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        int AddQuestion(int id, TestQuestionModelDto question);

        [OperationContract]
        TestDto GetTestDetails(int id);

        [OperationContract]
        bool UpdateTest(TestDto test);

        [OperationContract]
        void DeleteTest(TestDto test);

        [OperationContract]
        int AddTest(int courseId, TestDto test);

        [OperationContract]
        List<TestSignatureDto> GetAllTests();

        [OperationContract]
        List<TestSignatureDto> GetMyTests(int profileId);

        [OperationContract]
        IList<TestTypeModelDto> GetTestTypes();

        [OperationContract]
        bool AddAnswers(int questionId,List<TestQuestionAnswerDto> answers);
    }
}
