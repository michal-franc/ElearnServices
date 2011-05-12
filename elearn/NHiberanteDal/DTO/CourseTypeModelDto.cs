using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class CourseTypeModelDto : DtoBaseClass<CourseTypeModelDto, CourseTypeModel>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string TypeName { get; set; }
    }
}
