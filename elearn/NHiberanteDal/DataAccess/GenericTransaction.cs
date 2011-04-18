using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace NHiberanteDal.DataAccess
{
    public interface IGenericTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }

    public class GenericTransaction : IGenericTransaction
    {
        private readonly ITransaction _transaction;

        public GenericTransaction(ITransaction transaction)
        {
            _transaction = transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }
    }
}
