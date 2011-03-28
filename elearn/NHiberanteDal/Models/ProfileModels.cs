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
        public int ID;
        public List<ProfileModel> Users;
        public string GroupName;
        //AuthModel GroupAuth;
    }

    public class GroupTypeModel
    {
        public int ID;
        public string TypeName;
    }

    public class ProfileModel
    {
        int ID;
        // Linked with Forms Authentication
    }

    public class ProfileTypeModel
    {
        int ID;
        string TypeName;
    }

    public class PrivateMessageModel
    {
        public int ID;
        public ProfileModel Sender;
        public ProfileModel Receiver;
    }

    public class JournalModel
    {
        public int ID;
        public ProfileModel Owner;
        public string Name;
        public double AverageMark;
        public List<JournalMarkModel> Marks;
    }

    public class JournalMarkModel
    {
        public int ID;
        public string Name;
        public string Value;
    }

    #endregion
}