using NUnit.Framework;
using NHiberanteDal.DataAccess;

namespace NHibernateTests
{
    [TestFixture]
    class NHibernateTest
    {
        [Test]
        public  void Reset_schema()
        {
            DataAccess.ResetDb();
        }
    }
}
