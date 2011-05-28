using System;
using NUnit.Framework;

namespace NHibernateTests
{
    [TestFixture]
    public abstract class InMemoryTest
    {
        protected SqlLiteTestDBAccess _database;

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
