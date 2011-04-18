using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHiberanteDal.DataAccess;

namespace NHibernateTests
{
    [TestFixture]
    class DataAccesTests
    {
        [Test]
        public void Can_create_access_to_db_in_memory()
        {
            SqlLiteTestDBAccess _database = new SqlLiteTestDBAccess();
            using (var session = DataAccess.OpenSession())
            {
                Assert.That(session.Connection,Is.InstanceOf(typeof(System.Data.SQLite.SQLiteConnection)));
            }
        }

        [Test]
        public void By_default_creates_access_to_db_in_sql()
        {
            using (var session = DataAccess.OpenSession())
            {
                Assert.That(session.Connection, Is.InstanceOf(typeof(System.Data.SqlClient.SqlConnection)));
            }
        }

    }
}
