using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class SurveyQuestionModel
    {
        public virtual int ID { get; private set; }
        public virtual string QuestionText { get; set; }
        public virtual int TimesSelected { get; set; }
    }
}
