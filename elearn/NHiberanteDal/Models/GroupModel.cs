using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.Models
{
    public class GroupModel
    {
        public virtual int ID { get; private set; }
        public virtual IList<ProfileModel> Users { get; set; }
        public virtual string GroupName { get; set; }
        public virtual GroupTypeModel GroupType { get; set; }
        //AuthModel GroupAuth;
    }
}
