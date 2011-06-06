using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using NHiberanteDal.Models;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class TestQuestionAnswerDto : DtoBaseClass<TestQuestionAnswerDto, TestQuestionAnswer>
    {
        [DataMember]
        public  int ID { get; set; }
        [DataMember]
        public  string Text { get; set; }
        [DataMember]
        public  bool Correct { get; set; }
        [DataMember]
        public  int NumberSelected { get; set; }
    }
}
