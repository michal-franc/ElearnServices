using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NHiberanteDal.Models;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string Email { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public  IList<JournalModelDto> Journals { get; set; }

        [DataMember]
        public List<FinishedTestModelDto> FinishedTests { get; set; }

    }
}
