using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate.Linq;
using NHiberanteDal.DataAccess;
using NHibernate;
using Models;

namespace NHibernateTests
{
    [TestFixture]
    class ProfileServicesTests : TestBase
    {
        [Test]
        public  void CanAddProfile()
        {
           new ProfileService().AddProfile(new Models.ProfileModel());

           int count = 0;
           using (var session = DataAccess.OpenSession())
           {
               count = session.Linq<ProfileModel>().ToList().Count;
           }

           Assert.That(count,Is.EqualTo(1));

        }
    }
}
