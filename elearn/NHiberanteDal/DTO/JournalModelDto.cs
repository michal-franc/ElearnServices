using System.Collections.Generic;
using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class JournalModelDto : DtoBaseClass<JournalModelDto,JournalModel>
    {
        [DataMember]
        public  int ID { get; private set; }
        [DataMember]
        public  string Name { get; set; }
        [DataMember]
        public CourseSignatureDto Course { get; set; }
        [DataMember]
        public  List<JournalMarkModelDto> Marks { get; set; }
        [DataMember]
        public bool IsActive { get; set; }
    }
}
