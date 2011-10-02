using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace NHiberanteDal.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public T GetById(int id)
        {
            try
            {
                T obj;
                using (var session = DataAccess.OpenSession())
                {
                    obj = session.Get<T>(id);
                }
                return obj;
            }
            catch (Exception ex)
            {
                logger.Error("Error - Repository.GetById : {0}", ex.Message);
                return null;
            }         
        }

        public int? Add(T item)
        {
            int? addedItemId;

            using (var session = DataAccess.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        addedItemId = (int)session.Save(item);
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        addedItemId = null;
                        logger.Error("Error - Repository.Add : {0}", ex.Message);

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
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(item);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        logger.Error("Error - Repository.Remove : {0}", ex.Message);
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
            using (var session = DataAccess.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Update(item);
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        logger.Error("Error - Repository.Update : {0}", ex.Message);
                    }
                    finally
                    {
                        transaction.Dispose();
                    }
                }
            }
            return false;
        }

        public int GetCount()
        {
            try
            {
                int count;
                using (var session = DataAccess.OpenSession())
                {
                    count = session.CreateCriteria(typeof(T))
                       .List<T>().Count;
                }
                return count;
            }
            catch (Exception ex)
            {
                logger.Error("Error - Repository.GetCount : {0}",ex.Message);
                return 0;
            }
        }
        public IList<T> GetAll()
        {
            try
            {
                IList<T> returnedList;
                using (var session = DataAccess.OpenSession())
                {
                    returnedList = session.CreateCriteria(typeof(T)).SetCacheable(true).List<T>();
                }
                return returnedList;
            }
            catch (Exception ex)
            {
                logger.ErrorException("Error - Repository.GetAll : ", ex);
                return new List<T>();
            }
        }

        public IList<T> GetByParameterEqualsFilter(string parameterName, object value)
        {
            try
            {
                IList<T> returnedList;
                using (var session = DataAccess.OpenSession())
                {
                    returnedList = session.CreateCriteria(typeof(T)).Add(Restrictions.Eq(parameterName, value)).List<T>();
                }
                return returnedList;
            }
            catch (Exception ex)
            {
                logger.Error("Error - Repository.GetByParameterEqualsFilter : {0}", ex.Message);
                return new List<T>();
            }
        }

        public IList<T> GetByQuery(string query)
        {
            try
            {

                IList<T> returnedList;
                using (var session = DataAccess.OpenSession())
                {
                    returnedList = session.CreateQuery(query).List<T>();
                    session.Flush();
                }
                return returnedList;
            }
            catch (Exception ex)
            {
                logger.Error("Error - Repository.GetByQuery : {0}", ex.Message);
                return new List<T>();
            }
        }

        public IList<T> GetByQueryObject(IQueryObject query)
        {
            try
            {
                return GetByQuery(query.Query);

            }
            catch (Exception ex)
            {
                logger.Error("Error - Repository.GetByQueryObject : {0}", ex.Message);
                return new List<T>();
            }
        }
    }
}
