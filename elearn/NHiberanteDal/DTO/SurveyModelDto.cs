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
    }
}
