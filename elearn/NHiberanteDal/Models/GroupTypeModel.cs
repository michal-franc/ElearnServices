using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class GroupTypeModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string TypeName { get; set; }
    }
}
