using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace NHiberanteDal.Models
{
    public class ProfileModel : IModel
    {
        public virtual int ID { get; private set; }
        public virtual string Name { get; set; }
        // Linked with Forms Authentication
    }
}