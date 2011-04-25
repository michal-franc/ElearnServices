using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class TestSignatureDto : DtoBaseClass<TestSignatureDto,TestModel>
    {
        [DataMember]
        public int ID { get; private set; }
        [DataMember]
        public  string Name { get; set; }
        [DataMember]
        public  DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime EditDate { get; set; }
        [DataMember]
        public  ProfileModelDto Author { get; set; }
        [DataMember]
        public TestTypeModelDto TestType { get; set; }
    }
}
