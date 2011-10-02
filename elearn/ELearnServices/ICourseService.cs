using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // : You can use the "Rename" command on the "Refactor" menu to change the interface name "ICourseService" in both code and config file together.
    [ServiceContract]
    public interface ICourseService
    {
        IList<CourseDto> GetAll();

        [OperationContract]
        CourseDto GetById(int id);

        [OperationContract]
        [AspNetCacheProfile("CacheFor60Seconds")]
        [WebGet]
        IList<CourseSignatureDto> GetAllSignatures();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Get?id={profileId}")]
        IList<CourseSignatureDto> GetCourseSignaturesByProfileId(int profileId);

        [OperationContract]
        bool Remove(int id);

        [OperationContract]
        List<CourseDto> GetByName(string value);

        [OperationContract]
        List<CourseDto> GetByCourseType(CourseTypeModelDto testCourseType);

        [OperationContract]
        [WebInvoke( BodyStyle=WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        bool Update(CourseDto updatedCourse, bool reupload);

        [OperationContract]
        int? AddCourse(CourseDto newCourse);

        [OperationContract]
        List<CourseTypeModelDto> GetAllCourseTypes();

        [OperationContract]
        int? AddShoutBoxMessage(ShoutBoxMessageModelDto msg);

        [OperationContract]
        [WebInvoke( BodyStyle=WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        IList<ShoutBoxMessageModelDto> GetLatestShoutBoxMessages(int shoutBoxId, int numberOfMessage);

        [OperationContract]
        [WebInvoke( BodyStyle=WebMessageBodyStyle.Wrapped , ResponseFormat = WebMessageFormat.Json)]
        bool CheckPassword(int courseId, string password);

        [OperationContract]
        List<CourseDto> GetByProfileId(int profileId);
    }
}
