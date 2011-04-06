using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;

namespace NHiberanteDal.DataAccess
{
    public static class Repository<T>
        where T : class
    {
        public static void Add(T model)
        {
            DataAccess.InTransaction(session => session.Save(model));
        }

        public static int GetCount()
        {
            int count = 0;
            using (var session = DataAccess.OpenSession())
            {
                count = session.Linq<T>().ToList().Count;
            }

            return count;
        }

        public static T GetByID(int p)
        {
            T returnedObject = null;
            using (var session = DataAccess.OpenSession())
            {
                returnedObject = session.Get<T>(p);
            }

            return returnedObject;
        }
    }
}
