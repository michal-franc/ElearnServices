using System;
using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class LearningMaterialSignatureDto :  DtoBaseClass<LearningMaterialSignatureDto,LearningMaterialModel>
    {
        [DataMember]
        public virtual int ID { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string IconName { get; set; }
        [DataMember]
        public string Goals { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime UpdateDate { get; set; }
        [DataMember]
        public string VersionNumber { get; set; }
        [DataMember]
        public string Summary { get; set; }
        [DataMember]
        public string Links { get; set; }
    }
}
