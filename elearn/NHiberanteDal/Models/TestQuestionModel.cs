using System.Collections.Generic;

namespace NHiberanteDal.Models
{
    public class TestQuestionModel : IModel
    {
        public virtual int ID { get; set; }
        public virtual string QuestionText { get; set; }
        public virtual string QuestionLabel { get; set; }
        public virtual IList<TestQuestionAnswer> Answers { get; set; }

        public TestQuestionModel()
        {
           Answers = new List<TestQuestionAnswer>();
        }
    }
}
