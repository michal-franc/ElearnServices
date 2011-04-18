using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHiberanteDal.DataAccess;

namespace NHibernateTests
{
    [TestFixture]
    class NHibernateTest
    {
        [Test]
        public  void Reset_Schema()
        {
            DataAccess.ResetDb();
        }
    }
}
