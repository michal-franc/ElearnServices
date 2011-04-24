using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ELearnServices;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;
using NHiberanteDal.DTO;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    class JournalServiceTest : InMemoryWithSampleData
    {
        [Test]
        public void Can_Add_Mark()
        {
            #region Arrange
            JournalMarkModelDto markDto = new JournalMarkModelDto() { Name="test mark", Value="0" };
            #endregion

            #region Act

            bool addOk = new JournalService().AddMark(1,markDto);

            JournalModel journal = null;
            using (var session = DataAccess.OpenSession())
            {
                journal = session.Get<JournalModel>(1);

            #endregion

            #region Assert
            Assert.That(addOk, Is.True);
            Assert.That(journal.Marks.Count,Is.EqualTo(1));
            #endregion
            };
        }

        [Test]
        public void Can_Get_Journal_Details()
        {
            #region Arrange
            #endregion

            #region Act

            var journal = new JournalService().GetJournalDetails(1);

            #endregion

            #region Assert
            Assert.That(journal, Is.Not.Null);
            Assert.That(journal.Name, Is.EqualTo("test journal"));
            Assert.That(journal.Marks.Count, Is.EqualTo(1));
            Assert.That(journal.Marks.First().Name, Is.EqualTo("Zaliczenie"));
            #endregion
        }			
    }
}
