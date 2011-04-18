using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NHiberanteDal.Models
{
    public class ShoutboxModel : IModel
    {
        public virtual int ID { get;private set; }
       
        //Many
        public virtual IList<ShoutBoxMessageModel> Messages { get; set; }
    }
}