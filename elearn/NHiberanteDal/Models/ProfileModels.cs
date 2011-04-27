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
        public virtual string Role { get; set; }
        public virtual string Email { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual IList<JournalModel> Journals { get; set; }
    }
}