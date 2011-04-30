using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class PrivateMessageModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string Text { get; set; }
        public virtual bool IsNew { get; set; }
        public virtual ProfileModel Sender { get; set; }
        public virtual ProfileModel Receiver { get; set; }
    }
}
