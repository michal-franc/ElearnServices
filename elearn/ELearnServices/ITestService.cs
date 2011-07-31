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
        bool DeleteTestQuestion(int id);

        [OperationContract]
        TestDto GetTestDetails(int id);

        [OperationContract]
        bool UpdateTest(TestDto test);

        [OperationContract]
        bool DeleteTest(int id);

        [OperationContract]
        int AddTestToCourse(int courseId, TestDto test);

        [OperationContract]
        int AddTestToLearningMaterial(int learningMaterialId, TestDto test);

        [OperationContract]
        TestQuestionModelDto GetTestQuestion(int id);

        [OperationContract]
        bool UpdateTestQuestion(TestQuestionModelDto model);

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
