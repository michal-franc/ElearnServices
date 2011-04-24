using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NHiberanteDal.DTO
{
    [DataContract]
    public class TestQuestionAnswerDto
    {
        [DataMember]
        public  int ID { get; private set; }
        [DataMember]
        public  string Text { get; set; }
        [DataMember]
        public  bool Correct { get; set; }
        [DataMember]
        public  int NumberSelected { get; set; }
    }
}
