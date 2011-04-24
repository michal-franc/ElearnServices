using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public double AverageMark { get; set; }
        [DataMember]
        public CourseSignatureDto Course { get; set; }
        [DataMember]
        public  IList<JournalMarkModelDto> Marks { get; set; }
    }
}
