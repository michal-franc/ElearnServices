using System;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class CourseSignatureDto : DtoBaseClass<CourseSignatureDto,CourseModel>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public  string Name { get; set; }
        [DataMember]
        public  DateTime CreationDate { get; set; }
        [DataMember]
        public  string Logo { get; set; }
        [DataMember]
        public  string ShortDescription { get; set; }

        [DataMember]
        public  CourseTypeModelDto CourseType { get; set; }

        public override string ToString()
        {
            return String.Format("{0}:{1}",ID,Name);
        }
    }
}
