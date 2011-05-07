using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICourseService" in both code and config file together.
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
        IList<TestSignatureDto> GetAllTestsSignatures(int id);

        [OperationContract]
        bool Remove(int id);

        [OperationContract]
        List<CourseDto> GetByName(string value);

        [OperationContract]
        List<CourseDto> GetByCourseType(CourseTypeModelDto _testCourseType);

        [OperationContract]
        bool Update(CourseDto updatedCourse);

        [OperationContract]
        int AddCourse(CourseDto newCourse);
    }
}
