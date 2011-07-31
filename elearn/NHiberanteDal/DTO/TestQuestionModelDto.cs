using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class TestQuestionModelDto : DtoBaseClass<TestQuestionModelDto,TestQuestionModel>
    {
        [DataMember]
        public  int ID { get;set; }
        [DataMember]
        public  string QuestionText { get; set; }
        [DataMember]
        public string QuestionLabel { get; set; }
        [DataMember]
        public  List<TestQuestionAnswerDto> Answers { get; set; }
    }
}
