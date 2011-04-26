using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using ELearnServices;
using NHiberanteDal.DTO;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    class GroupServiceTests : InMemoryWithSampleData
    {
        [Test]
        public void Can_Get_All_Groups()
        {
            #region Arrange
            var group = new GroupModel() { GroupName="test", GroupType=_testGroupType};
            DataAccess.InTransaction(session =>
            {
                session.Save(group);
            });
            #endregion

            #region Act

            var groups = new GroupService().GetAllGroups();

            #endregion

            #region Assert
            Assert.That(groups.First(), Is.InstanceOf(typeof(GroupModelDto)));
            Assert.That(groups.Count, Is.EqualTo(2));
            #endregion
        }


        [Test]
        public void Can_Get_Group()
        {
            #region Arrange
            #endregion

            #region Act
            var group = new GroupService().GetGroup(1);
            #endregion

            #region Assert
            Assert.That(group, Is.Not.Null);
            Assert.That(group.GroupName, Is.EqualTo("test"));
            #endregion
        }


        [Test]
        public void Can_Add_Group()
        {
            #region Arrange
            var group = new GroupModel() { GroupName = "new group", GroupType=_testGroupType };
            #endregion

            #region Act

            int id = new GroupService().AddGroup(GroupModelDto.Map(group));
            GroupModel groupModel= null;
            DataAccess.InTransaction(session =>
            {
                groupModel = session.Get<GroupModel>(id);
            });
            #endregion

            #region Assert
            Assert.That(groupModel, Is.Not.Null);
            Assert.That(groupModel.GroupName, Is.EqualTo("new group"));
            #endregion
        }


        [Test]
        public void Can_Update_Group()
        {
            #region Arrange
            GroupModel group = null;
            DataAccess.InTransaction(session =>
            {
                group = session.Get<GroupModel>(1);
            });
            group.GroupName = "update test";
            #endregion

            #region Act

            bool updateOk = new GroupService().UpdateGroup(GroupModelDto.Map(group));

            #endregion
            GroupModel testGroup = null;
            DataAccess.InTransaction(session =>
            {
                testGroup = session.Get<GroupModel>(1);
            });

            #region Assert
            Assert.That(updateOk, Is.True);
            Assert.That(testGroup.GroupName, Is.EqualTo("update test"));
            #endregion
        }


        [Test]
        public void Can_Delete_Group()
        {
            #region Arrange
            var group = new GroupModel() { GroupName = "delete test", GroupType=_testGroupType };
            int id = -1;
            DataAccess.InTransaction(session =>
            {
                id = (int)session.Save(group);
            });
            #endregion

            #region Act
            var groupDto = GroupModelDto.Map(group);
            groupDto.ID = id;
            bool deleteOk = new GroupService().DeleteGroup(groupDto);

            IList<GroupModel> groups = null;

            DataAccess.InTransaction(session =>
            {
                groups = session.CreateCriteria(typeof(GroupModel)).List<GroupModel>();
            });

            #endregion

            #region Assert
            Assert.That(deleteOk, Is.True);
            Assert.That(groups.Count, Is.EqualTo(1));
            #endregion
        }			
    }
}
