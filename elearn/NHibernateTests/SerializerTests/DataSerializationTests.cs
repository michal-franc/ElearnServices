using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using System.Runtime.Serialization;
using NUnit.Framework;
using System.IO;

namespace NHibernateTests.SerializerTests
{
    [TestFixture]    
    class DataSerializationTests
    {
        [Test]
        public void DataContractSerialization_will_change_the_type_of_a_Collection()
        {
            ForumModel forum; 
            using (var session = DataAccess.OpenSession())
            {
                forum = session.Get<ForumModel>(1);


            Assert.AreEqual(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>), forum.Topics.GetType());

            List<Type> knownTypes = new List<Type>();
            knownTypes.Add(typeof(TopicModel));
            knownTypes.Add(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>));
            knownTypes.Add(typeof(NHibernate.Impl.CollectionFilterImpl));
            DataContractSerializer serializer = new
                     DataContractSerializer(typeof(ForumModel), knownTypes);

            //serialize company to a memory stream
            Stream stream = new MemoryStream();
            serializer.WriteObject(stream, forum);
            Console.WriteLine();
            //deserialize the memory stream back to a company
            stream.Position = 0;
            forum = (ForumModel)serializer.ReadObject(stream);

            Assert.AreNotEqual(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>), forum.Topics.GetType());
            Assert.AreEqual(typeof(TopicModel[]), forum.Topics.GetType());
            Assert.AreEqual(forum.Topics[0].Text,"test");
            }
        }

        [Test]
        public void NetDataContractSerialization_will_Not_change_the_type_of_a_Collection()
        {
            ForumModel forum;
            using (var session = DataAccess.OpenSession())
            {
                forum = session.Get<ForumModel>(1);

                //company.EmployeesList made public for the purpose of 
                //this demo
                Assert.AreEqual(typeof(NHibernate.Collection.Generic.PersistentGenericBag<TopicModel>), forum.Topics.GetType());

                NetDataContractSerializer serializer = new NetDataContractSerializer();

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
