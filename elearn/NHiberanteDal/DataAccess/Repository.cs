using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace NHiberanteDal.DataAccess
{
    public class Repository<T>
    {
        public T GetById(int id)
        {
            T obj;

            using (var session = DataAccess.OpenSession())
            {
                obj = session.Get<T>(id);
            }

            return obj;
        }
        public int Add(T item)
        {
            int addedItemId;

            using (var session = DataAccess.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        addedItemId = (int)session.Save(item);
                        transaction.Commit();
                    }
                    catch(Exception)
                    {
                        transaction.Rollback();
                        addedItemId = -1;
                    }

                    finally
                    {
                        transaction.Dispose();

                    }
                }
            }

            return addedItemId;
        }

        public void Remove(T item)
        {

            using (var session = DataAccess.OpenSession())
            {

                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(item);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
            }
        }

        public bool Update(T item)
        {
            bool ok;
            using (var session = DataAccess.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(item);
                        transaction.Commit();
                        ok = true;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        ok = false;
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
            }
            return ok;
        }

        public int GetCount()
        {
            int count;
            using (var session = DataAccess.OpenSession())
            {
                count = session.CreateCriteria(typeof(T))
                   .List<T>().Count;
            }
            return count;
        }
        public IList<T> GetAll()
        {
            IList<T> returnedList;
            using (var session = DataAccess.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).List<T>();
            }
            return returnedList;
        }

        public IList<T> GetByParameterEqualsFilter(string parameterName, object value)
        {
            IList<T> returnedList;
            using (var session = DataAccess.OpenSession())
            {
                returnedList = session.CreateCriteria(typeof(T)).Add(Restrictions.Eq(parameterName, value)).List<T>();
            }
            return returnedList;
        }

        public IList<T> GetByQuery(string query)
        {
            IList<T> returnedList;
            using (var session = DataAccess.OpenSession())
            {
                returnedList = session.CreateQuery(query).List<T>();
                session.Flush();
            }
            return returnedList;
        }

        public IList<T> GetByQueryObject(IQueryObject query)
        {
            return GetByQuery(query.Query);
        }
    }
}
