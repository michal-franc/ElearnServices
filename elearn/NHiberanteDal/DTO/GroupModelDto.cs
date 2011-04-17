using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class GroupModelDto : DtoBaseClass<GroupModelDto,GroupModel>
    {
        [DataMember]
        public  int ID { get; private set; }
        [DataMember]
        public  string GroupName { get; set; }
        [DataMember]
        public  GroupTypeModelDto GroupType { get; set; }
        public  IList<ProfileModel> Users { get; set; }
    }
}
