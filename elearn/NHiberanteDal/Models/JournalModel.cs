using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class JournalModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string Name { get; set; }
        public virtual double AverageMark { get; set; }
        public virtual CourseModel Course { get; set; }
        public virtual IList<JournalMarkModel> Marks { get; set; }
    }
}
