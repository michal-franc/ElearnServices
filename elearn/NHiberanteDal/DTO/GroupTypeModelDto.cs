using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class GroupTypeModelDto : DtoBaseClass<GroupTypeModelDto,GroupTypeModel>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string TypeName { get; set; }
    }
}
