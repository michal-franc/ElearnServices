using System.Linq;
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
        public void Can_add_mark()
        {
            #region Arrange
            var markDto = new JournalMarkModelDto{ Name="test mark", Value="0" };
            #endregion

            #region Act

            bool addOk = new JournalService().AddMark(1,markDto);

            using (var session = DataAccess.OpenSession())
            {
            var journal = session.Get<JournalModel>(1);

            #endregion

            #region Assert
            Assert.That(addOk, Is.True);
            Assert.That(journal.Marks.Count,Is.EqualTo(1));
            #endregion
            }
        }

        [Test]
        public void Can_get_journal_details()
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


        [Test]
        public void Can_delete_mark()
        {
            #region Arrange

            JournalMarkModel markModel;
            using (var session = DataAccess.OpenSession())
            {
                markModel = new JournalMarkModel() {Name = "test", Value = "5.0"};
                session.SaveOrUpdate(markModel);
            }
            #endregion

            #region Act

            var ok = new JournalService().RemoveMark(markModel.ID);

            #endregion

            #region Assert
            Assert.IsTrue(ok);
            #endregion
        }
				
    }
}
