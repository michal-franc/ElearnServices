using System;
using System.Collections.Generic;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using System.Runtime.Serialization;
using NUnit.Framework;
using System.IO;

namespace NHibernateTests.SerializerTests
{
    [TestFixture]    
    class DataSerializationTests : InMemoryTest
    {
        [SetUp]
        public void SetUp()
        {
            using(var session = DataAccess.OpenSession())
            {
                session.Save(new ForumModel { Author="test", Name="test" });
            }
        }

        [Test]
        public void Data_contract_serialization_will_change_the_type_of_a_collection()
        {
            using (var session = DataAccess.OpenSession())
            {
                var forum = session.Get<ForumModel>(1);


            Assert.AreEqual(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>), forum.Topics.GetType());

            var knownTypes = new List<Type>
                                 {
                                     typeof (TopicModel),
                                     typeof (NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>),
                                     typeof (NHibernate.Impl.CollectionFilterImpl)
                                 };
                var serializer = new   DataContractSerializer(typeof(ForumModel), knownTypes);

            //serialize company to a memory stream
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, forum);
            Console.WriteLine();
            //deserialize the memory stream back to a company
            stream.Position = 0;
            forum = (ForumModel)serializer.ReadObject(stream);

            Assert.AreNotEqual(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>), forum.Topics.GetType());
            Assert.AreEqual(typeof(TopicModel[]), forum.Topics.GetType());
            }
        }

        [Test]
        public void Net_data_contract_serialization_will_not_change_the_type_of_a_collection()
        {
            using (var session = DataAccess.OpenSession())
            {
                var forum = session.Get<ForumModel>(1);

                //company.EmployeesList made public for the purpose of 
                //this demo
                Assert.AreEqual(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>), forum.Topics.GetType());

                var serializer = new NetDataContractSerializer();

                //serialize company to a memory stream
                Stream stream = new MemoryStream();
                serializer.WriteObject(stream, forum);
                Console.WriteLine();
                //deserialize the memory stream back to a company
                stream.Position = 0;
                forum = (ForumModel)serializer.ReadObject(stream);

                Assert.AreEqual(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>), forum.Topics.GetType());
            }
        }
    }
}
