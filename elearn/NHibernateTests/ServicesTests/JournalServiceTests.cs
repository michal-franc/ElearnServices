using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ELearnServices;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    class JournalServiceTest : InMemoryWithSampleData
    {
        [Test]
        public void Can_Add_Mark()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_Delete_Mark()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_Edit_Mark()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
        }

        [Test]
        public void Can_Get_All_Journals_Signatures()
        {
            #region Arrange
            #endregion

            #region Act

            Assert.Fail();

            #endregion

            #region Assert
            #endregion
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
