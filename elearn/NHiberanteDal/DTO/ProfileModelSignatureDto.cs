using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class ProfileModelSignatureDto : DtoBaseClass<ProfileModelSignatureDto,ProfileModel>
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string Role { get; set; }

        [DataMember]
        [Required]
        public string Email { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}
