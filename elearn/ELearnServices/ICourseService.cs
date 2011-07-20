using System.Collections.Generic;
using System.ServiceModel;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // : You can use the "Rename" command on the "Refactor" menu to change the interface name "ICourseService" in both code and config file together.
    [ServiceContract]
    public interface ICourseService
    {
        [OperationContract]
        IList<CourseDto> GetAll();

        [OperationContract]
        CourseDto GetById(int id);

        [OperationContract]
        IList<CourseSignatureDto> GetAllSignatures();

        [OperationContract]
        IList<CourseSignatureDto> GetCourseSignaturesByProfileId(int profileId);

        [OperationContract]
        IList<TestSignatureDto> GetAllTestsSignatures(int id);

        [OperationContract]
        bool Remove(int id);

        [OperationContract]
        List<CourseDto> GetByName(string value);

        [OperationContract]
        List<CourseDto> GetByCourseType(CourseTypeModelDto testCourseType);

        [OperationContract]
        bool Update(CourseDto updatedCourse, bool reupload);

        [OperationContract]
        int? AddCourse(CourseDto newCourse);

        [OperationContract]
        List<CourseTypeModelDto> GetAllCourseTypes();

        [OperationContract]
        int? AddShoutBoxMessage(ShoutBoxMessageModelDto msg);

        [OperationContract]
        IList<ShoutBoxMessageModelDto> GetLatestShoutBoxMessages(int shoutBoxId, int numberOfMessage);

        [OperationContract]
        bool CheckPassword(int courseId, string password);

        [OperationContract]
        List<CourseDto> GetByProfileId(int profileId);
    }
}
