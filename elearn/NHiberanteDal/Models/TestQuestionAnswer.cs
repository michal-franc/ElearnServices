using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class TestQuestionAnswer
    {
        public virtual int ID { get; private set; }
        public virtual string Text { get; set; }
        public virtual bool Correct { get; set; }
        public virtual int NumberSelected { get; set; }
    }
}
