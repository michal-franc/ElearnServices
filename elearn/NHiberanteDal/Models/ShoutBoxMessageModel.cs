using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class ShoutBoxMessageModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string Author { get; set; }
        public virtual DateTime TimePosted { get; set; }
        public virtual string Message { get; set; }
    }
}
