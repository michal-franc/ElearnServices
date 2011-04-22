using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class TestTypeModelDto : DtoBaseClass<TestTypeModelDto,TestTypeModel>
    {
        [DataMember]
        public  int ID { get; private set; }
        [DataMember]
        public  string TypeName { get; set; }
    }
}
