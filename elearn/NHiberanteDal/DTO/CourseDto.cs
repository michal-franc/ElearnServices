using System;
using System.Collections.Generic;
using NHiberanteDal.Models;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class CourseDto : DtoBaseClass<CourseDto,CourseModel>
    {

        public static int DefaultGroupTypeId = 2;
        public static int DefaultCourseTypeId = 3;


        [DataMember]
        public int ID { get; set; }
        [Required]
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public string Logo { get; set; }
        [DataMember]
        public  string Description { get; set; }
        [DataMember]
        public string News { get; set; }
        [DataMember]
        public string ShortDescription { get; set; }
        [DataMember]
        public CourseTypeModelDto CourseType { get; set; }
        [DataMember]
        public int CourseTypeId { get; set; }
        [DataMember]
        public  GroupModelDto Group { get; set; }
        [DataMember]
        public ShoutboxModelDto ShoutBox { get; set; }
        [DataMember]
        public bool IsPasswordProtected { get; set; }
        [DataMember]
        public List<LearningMaterialSignatureDto> LearningMaterials { get; set; }
    }

}
