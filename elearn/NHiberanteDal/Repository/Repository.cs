using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace NHiberanteDal
{
    public static class Repository<T> 
        where T : class
    {
        public static T GetById(int id)
        {
            T klient;

            klient = GetByFilter("Id", id).FirstOrDefault();

            return klient;
        }
        public static int Add(T item)
        {
            int addedItemId;

            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    addedItemId = (int)session.Save(item);
                    transaction.Commit();
                    session.Flush();
                }
            }

            return addedItemId;
        }

        public static void AddById(T item, int Id)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(item, Id);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public static void Remove(T item)
        {

            using (var session = SessionFactory.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(item);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public static void Update(T item)
        {

            using (var session = SessionFactory.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(item);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public static void UpdateById(T item, int Id)
        {

            using (var session = SessionFactory.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(item, Id);
                    transaction.Commit();
                    session.Flush();
                }
            }
        }

        public static int GetCount()
        {
            int count = 0;
            using (var session = SessionFactory.OpenSession())
            {
                count = session.CreateCriteria(typeof(T))
                   .List<T>().Count;
                session.Flush();
            }
            return count;
        }
        public static IList<T> GetAll()
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).List<T>();
                session.Flush();
            }
            return returnedList;
        }

        public static IList<T> GetByFilter(string parameterName, object value)
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).Add(Expression.Eq(parameterName, value)).List<T>();
                session.Flush();
            }
            return returnedList;
        }


        public static IList<T> GetByCriteria(ICriterion criteria)
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).Add(criteria).List<T>();
                session.Flush();
            }
            return returnedList;
        }

        public static IList<T> GetByQuery(string query)
        {
            IList<T> returnedList = null;
            using (var session = SessionFactory.OpenSession())
            {
                returnedList = session.CreateQuery(query).List<T>();
                session.Flush();
            }
            return returnedList;
        }

        public static IQueryable<T> SelectUserData(Guid id)
        {
            IQueryable<T> table;

            using (var session = SessionFactory.OpenSession())
            {
                table = session.GetNamedQuery("Select_UserData").SetGuid("userid", id).List<T>().AsQueryable<T>();
                session.Flush();
            }

            return table;
        }
    }
}
