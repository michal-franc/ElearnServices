using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using AutoMapper;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class CourseDto : DtoBaseClass<CourseDto,CourseModel>
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public string Logo { get; set; }
        [DataMember]
        public  string Description { get; set; }
        [DataMember]
        public CourseTypeModelDto CourseType { get; set; }
        [DataMember]
        public virtual GroupModelDto Group { get; set; }
        [DataMember]
        public virtual ForumModelDto Forum { get; set; }
        [DataMember]
        public ShoutboxModelDto ShoutBox { get; set; }
        [DataMember]
        public SurveyModel LatestSurvey {get;set;}
    }
}
