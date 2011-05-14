﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using FluentNHibernate.Testing;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using System.Collections;

namespace NHibernateTests
{

    public class IDEqualityComparer : IEqualityComparer
    {
        new public bool Equals(object x, object y)
        {
            #region Comparer

            if (x == null || y == null)
            {
                return false;
            }
            if (x is IModel && y is IModel)
            {
                return ((IModel)x).ID == ((IModel)y).ID;
            }

            return x.Equals(y); 
            #endregion
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    class NHibernateMappingTests : InMemoryTest
    {
        GroupTypeModel _groupType = new GroupTypeModel() { TypeName = "test" };
        ContentTypeModel _contentType = new ContentTypeModel() { TypeName="test" };
        CourseTypeModel _courseType = new CourseTypeModel() { TypeName = "test" };
        TestTypeModel _testType = new TestTypeModel() { TypeName = "test" };
        ProfileModel _testProfile = new ProfileModel() { Email="test", Name = "test" };

        
        [SetUp]
        public void SetUp()
        {
            using (var session = DataAccess.OpenSession())
            {
                session.Save(_groupType);
                session.Save(_contentType);
                session.Save(_courseType);
                session.Save(_testType);
                session.Save(_testProfile);
            }
        }

        [Test]
        public void Can_map_entity_course_type()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<CourseTypeModel>(session)
                   .CheckProperty(c => c.TypeName, "test")
                   .VerifyTheMappings();
            }

            #endregion
        }
        
        [Test]
        public void Can_map_entity_course()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<CourseModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.Description, "test")
                   .CheckProperty(c => c.CreationDate, new DateTime(2010, 10, 1))
                   .CheckProperty(c => c.Name, "test")
                   .CheckReference(c => c.CourseType, _courseType)
                   .CheckReference(c => c.Group, new GroupModel()
                   {
                       GroupName = "test",
                       GroupType = _groupType
                   })
                   .CheckReference(c => c.Forum, new ForumModel() { Name = "test", Author = "test" })
                   .CheckReference(c => c.ShoutBox, new ShoutboxModel() { })
                   .CheckList<ContentModel>(c => c.Contents,
                   new List<ContentModel>() 
                        { 
                            new ContentModel(){ Name="test", CreationDate=new DateTime(2010,10,1), ContentUrl="test" , Type=_contentType} 
                        }
                   )
                   .CheckList<SurveyModel>(c => c.Surveys,
                   new List<SurveyModel>() 
                        { 
                            new SurveyModel(){ SurveyText="test"} 
                        }
                   )
                   .CheckList<TestModel>(c => c.Tests,
                   new List<TestModel>() 
                        { 
                            new TestModel()
                            { 
                                Author=_testProfile, Name="test", 
                                CreationDate= new DateTime(2010,10,1) ,
                                TestType=_testType
                            } 
                        }
                   )
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_content()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<ContentModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.Text, "test")
                   .CheckProperty(c => c.CreationDate, new DateTime(2010, 10, 1))
                   .CheckProperty(c => c.Name, "test")
                   .CheckProperty(c => c.ContentUrl, "test")
                   .CheckProperty(c => c.EditDate, new DateTime(2010, 10, 1))
                   .CheckProperty(c => c.DownloadNumber, 2)
                   .CheckReference(c => c.Type, _contentType)
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_content_type()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<CourseTypeModel>(session)
                   .CheckProperty(c => c.TypeName, "test")
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_forum()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<ForumModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.Name, "test")
                   .CheckProperty(c => c.Author, "test")
                   .CheckList<TopicModel>(c => c.Topics,
                   new List<TopicModel>() 
                        { 
                            new TopicModel(){ Text="test"}
                        }
                   )
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_topic()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<TopicModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.Text, "test")
                   .CheckList<PostModel>(c => c.Posts,
                   new List<PostModel>() 
                        { 
                            new PostModel(){ Text="test"}
                        }
                   )
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_post()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<PostModel>(session)
                   .CheckProperty(c => c.Text, "test")
                   .VerifyTheMappings();
            }

            #endregion
        }


        [Test]
        public void Can_map_entity_group()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<GroupModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.GroupName, "test")
                   .CheckReference(c => c.GroupType, _groupType)
                   .CheckList<ProfileModel>(c => c.Users,
                   new List<ProfileModel>() 
                        { 
                            new ProfileModel(){ Email="test", Name="test"}
                        }
                   )
                   .VerifyTheMappings();
            }


            #endregion
        }

        [Test]
        public void Can_map_entity_group_type()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<GroupTypeModel>(session)
                   .CheckProperty(c => c.TypeName, "test")
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_profile()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<ProfileModel>(session)
                   .CheckProperty(c => c.Name, "test")
                   .CheckProperty(c => c.Email, "test")
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_profile_type()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<ProfileTypeModel>(session)
                   .CheckProperty(c => c.TypeName, "test")
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_private_message()
        {
            #region Act
            using (var session = DataAccess.OpenSession())
            {

                new PersistenceSpecification<PrivateMessageModel>(session, new IDEqualityComparer())
                   .CheckProperty(c=>c.IsNew,true)
                   .CheckProperty(c=>c.Text,"test text")
                   .CheckReference(c => c.Receiver, _testProfile)
                   .CheckReference(c => c.Sender, _testProfile)
                   .VerifyTheMappings();


            }
            #endregion
        }

        [Test]
        public void Can_map_entity_journal()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<JournalModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.Name, "test")
                   .CheckProperty(c => c.AverageMark, 3.45)
                   .CheckReference(c => c.Course, new CourseModel()
                   {
                       Name = "test",
                       CourseType = _courseType,
                       CreationDate = DateTime.Now,
                       Forum = new ForumModel() { Name = "test", Author = "test" },
                       Group = new GroupModel() { GroupName = "test", GroupType = _groupType },
                       ShoutBox = new ShoutboxModel() { }
                   })
                   .CheckList<JournalMarkModel>(c => c.Marks,
                   new List<JournalMarkModel>() 
                        { 
                            new JournalMarkModel(){ Value="1.2",Name="test"}
                        }
                   )
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_mark()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<JournalMarkModel>(session)
                   .CheckProperty(c => c.Name, "test")
                   .CheckProperty(c => c.Value, "1")
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_shoutBox()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<ShoutboxModel>(session, new IDEqualityComparer())
                   .CheckList<ShoutBoxMessageModel>(c => c.Messages,
                   new List<ShoutBoxMessageModel>() 
                        { 
                            new ShoutBoxMessageModel(){ Message="test", Author="test", TimePosted=new DateTime(2010,1,1)}
                        }
                   )
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_shoutbox_message()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<ShoutBoxMessageModel>(session)
                   .CheckProperty(c => c.Author, "test")
                   .CheckProperty(c => c.Message, "1")
                   .CheckProperty(c => c.ShoutBoxId, 1)
                   .CheckProperty(c => c.TimePosted,new DateTime(2010,1,1))
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_survey()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<SurveyModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.SurveyText, "test")
                   .CheckProperty(c => c.EndDate , new DateTime(2010,10,10))
                   .CheckProperty(c => c.DateCreated, new DateTime(2010, 10, 10))
                   .CheckList<SurveyQuestionModel>(c => c.Questions,
                   new List<SurveyQuestionModel>() 
                        { 
                            new SurveyQuestionModel(){ QuestionText="test", TimesSelected  = 1}
                        }
                   )
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_survey_question()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<SurveyQuestionModel>(session)
                   .CheckProperty(c => c.QuestionText, "test")
                   .CheckProperty(c => c.TimesSelected, 1)
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_test()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<TestModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.Name, "test")
                   .CheckProperty(c => c.CreationDate, new DateTime(2010,10,1))
                   .CheckProperty(c => c.EditDate, new DateTime(2010, 10, 1))
                   .CheckReference(c => c.Author, _testProfile)
                   .CheckReference(c => c.TestType, _testType)
                   .CheckList<TestQuestionModel>(c => c.Questions,
                   new List<TestQuestionModel>() 
                        { 
                            new TestQuestionModel(){ QuestionText="test"}
                        }
                   )

                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_test_type()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<TestTypeModel>(session)
                   .CheckProperty(c => c.TypeName, "test")
                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_test_question()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<TestQuestionModel>(session, new IDEqualityComparer())
                   .CheckProperty(c => c.QuestionText, "test")
                   .CheckList<TestQuestionAnswer>(c => c.Answers,
                   new List<TestQuestionAnswer>() 
                        { 
                            new TestQuestionAnswer(){ NumberSelected=1, Text="test", Correct=true}
                        }
                   )

                   .VerifyTheMappings();
            }

            #endregion
        }

        [Test]
        public void Can_map_entity_test_question_answer()
        {
            #region Act

            using (var session = DataAccess.OpenSession())
            {
                new PersistenceSpecification<TestQuestionAnswer>(session)
                   .CheckProperty(c => c.Correct, false)
                   .CheckProperty(c => c.Text, "test")
                   .CheckProperty(c => c.NumberSelected, 1)
                   .VerifyTheMappings();
            }

            #endregion
        }
				
				

    }
}
