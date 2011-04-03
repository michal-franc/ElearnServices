using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{

    public class SurveyModel
    {
        public virtual int ID { get; private set; }
        //public int IDCourse;
        //public int IDGroup;
        public virtual string SurveyText { get; set; }

        //Many
        public virtual List<SurveyQuestionModel> Questions { get; set; }

    }

    public class SurveyQuestionModel
    {
        public virtual int ID { get; private set; }
        public virtual string QuestionText { get; set; }
        public virtual int TimesSelected { get; set; }
    }
}
