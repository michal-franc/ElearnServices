using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class TestQuestionModel
    {
        public virtual int ID { get; private set; }
        public virtual string QuestionText { get; set; }
        public virtual IList<TestQuestionAnswer> Answers { get; set; }
    }
}
