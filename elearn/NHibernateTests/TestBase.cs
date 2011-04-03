using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHiberanteDal.DataAccess;

namespace NHibernateTests
{
    [TestFixture]
    public abstract class TestBase
    {
        protected  SqlLiteTestDBAccess _database;

        [SetUp]
        public void CreateSqlLiteDB()
        {

            _database = new SqlLiteTestDBAccess();
            Console.WriteLine("Test DataBase Created");
        }


        [TearDown]
        public void DestroySqlLiteDB()
        {
            _database.Dispose();
            Console.WriteLine("Test DataBaseDisposed");
        }
    }
}
