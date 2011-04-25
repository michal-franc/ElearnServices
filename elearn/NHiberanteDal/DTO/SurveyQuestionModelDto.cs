using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class SurveyQuestionModelDto
    {
        [DataMember]
        public  int ID { get; private set; }
        [DataMember]
        public  string QuestionText { get; set; }
        [DataMember]
        public  int TimesSelected { get; set; }
    }
}
