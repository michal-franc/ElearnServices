using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using Models;

namespace NHiberanteDal.DataAccess
{
    public class ProfileService
    {
        public void AddProfile(ProfileModel profile)
        {
            DataAccess.InTransaction(session => session.Save(profile));
        }

        public int GetCount()
        {
            int count = 0;
            using (var session = DataAccess.OpenSession())
            {
                count  =session.Linq<ProfileModel>().ToList().Count;
            }

            return count;
        }
    }
}
