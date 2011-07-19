using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class FileDto : DtoBaseClass<FileDto, FileModel>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string Address { get; set; }
    }
}
