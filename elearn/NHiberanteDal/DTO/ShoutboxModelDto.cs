using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class ShoutboxModelDto : DtoBaseClass<ShoutboxModelDto,ShoutboxModel>
    {
        [DataMember]
        public  int ID { get; private set; }
        //Many
        public  IList<ShoutBoxMessageModel> Messages { get; set; }
    }
}
