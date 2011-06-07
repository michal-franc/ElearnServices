using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class JournalMarkModel : IModel
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }
}
