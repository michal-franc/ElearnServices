using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class ProfileModelDto : DtoBaseClass<ProfileModelDto,ProfileModel>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Role { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public  IList<JournalModelDto> Journals { get; set; }
    }
}
