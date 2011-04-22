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
        public  string Name { get; set; }
        [DataMember]
        public  DateTime CreationDate { get; set; }
        [DataMember]
        public  string Author { get; set; }
        [DataMember]
        public TestTypeModelDto TestType { get; set; }
    }
}
