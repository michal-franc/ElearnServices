using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NHibernateTests
{
    [TestFixture]
    public class NHibernateInit
    {
        [Test]
        public void Test()
        {
            NHiberanteDal.Repository.SessionFactory.ResetSchema();
        }
    }
}
