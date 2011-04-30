using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    class SurveyModelDto : DtoBaseClass<SurveyModelDto,SurveyModel>
    {
        [DataMember]
        public int ID { get; private set; }
        [DataMember]
        public string SurveyText { get; set; }

        [DataMember]
        public  DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public  IList<SurveyQuestionModelDto> Questions { get; set; }

    }
}
