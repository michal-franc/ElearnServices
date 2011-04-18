using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{

    public class SurveyModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string SurveyText { get; set; }

        //Many
        public virtual IList<SurveyQuestionModel> Questions { get; set; }
    }

}
