using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;


namespace Models
{
    #region Models

    public class GroupModel
    {
        public virtual int ID { get; private set; }
        public virtual List<ProfileModel> Users { get; set; }
        public virtual string GroupName { get; set; }
        //AuthModel GroupAuth;
    }

    public class GroupTypeModel
    {
        public virtual int ID {get;private set;}
        public virtual string TypeName { get; set; }
    }

    public class ProfileModel
    {
        public virtual int ID { get; private set; }
        // Linked with Forms Authentication
    }

    public class ProfileTypeModel
    {
        public virtual int ID { get;private set; }
        public virtual string TypeName { get; set; }
    }

    public class PrivateMessageModel
    {
        public virtual int ID { get;private set; }
        public virtual ProfileModel Sender { get; set; }
        public virtual ProfileModel Receiver { get; set; }
    }

    public class JournalModel
    {
        public virtual int ID { get; private set; }
        public virtual ProfileModel Owner { get; set; }
        public virtual string Name { get; set; }
        public virtual double AverageMark { get; set; }
        public virtual List<JournalMarkModel> Marks { get; set; }
    }

    public class JournalMarkModel
    {
        public virtual int ID { get; private set; }
        public virtual string Name { get; set; }
        public virtual string Value { get; set; }
    }

    #endregion
}