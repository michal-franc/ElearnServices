using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class SectionDto : DtoBaseClass<SectionDto,SectionModel>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string IconName { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Text { get; set; }
    }
}
