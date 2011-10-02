using System;
using System.Collections.Generic;
using NUnit.Framework;
using NHiberanteDal.Models;
using NHiberanteDal.DataAccess;
using NHiberanteDal.DTO;

namespace NHibernateTests.ServicesTests
{
    [SetUpFixture]
    public class InMemoryWithSampleData : InMemoryTest
    {
        protected ShoutBoxMessageModel TestShoutBoxMessage;
        protected ShoutBoxMessageModel TestShoutBoxMessage1;
        protected CourseTypeModel TestCourseType;
        protected CourseTypeModel TestCourseType1;
        protected ContentTypeModel TestContentType;
        protected GroupTypeModel TestGroupType;
        protected GroupModel TestGroup;
        protected ForumModel TestForum;
        protected ShoutboxModel TestShoutBox;
        protected SurveyModel TestSurvey;
        protected SurveyModel TestLatestSurvey;
        protected CourseModel TestCourse1;
        protected CourseModel TestCourse2;
        protected CourseModel TestCourse3;
        protected TestModel TestTest;
        protected TestModel LatestTest;
        protected TestTypeModel TestTestType;
        protected ProfileModel TestPofile;
        protected TestQuestionAnswer TestQuestionAnswer;
        protected TestQuestionModel TestQuestion;
        protected JournalModel TestJournal;
        protected JournalMarkModel TestJournalMark;
        protected GroupTypeModel TestGroupType1;
        protected ContentModel TestContent;


        #region Learning Materials
        protected LearningMaterialModel LearningMaterialModel;
        #region Sections
        protected SectionModel SectionModel;
        #endregion

        #region Files
        protected FileModel FileModel;
        #endregion

        #endregion




        [SetUp]
        public void SetUp()
        {
            //Initializaing Mappings
            DtoMappings.Initialize();


            //Initializing Data
            TestShoutBoxMessage = new ShoutBoxMessageModel{Author = "test",Message = "test",ShoutBoxId = 1,TimePosted = new DateTime(2010,10,10,10,10,10)};
            TestShoutBoxMessage1 = new ShoutBoxMessageModel { Author = "test", Message = "testLatest", ShoutBoxId = 1, TimePosted = new DateTime(2011, 10, 10, 10, 10, 10) };
            TestQuestionAnswer = new TestQuestionAnswer {Correct = true,NumberSelected = 0, Text = "test"};
            TestQuestion = new TestQuestionModel
                                { QuestionText = "test question",QuestionLabel ="test", Answers = new List<TestQuestionAnswer>
                                                                                { TestQuestionAnswer } };
            TestTestType = new TestTypeModel { TypeName = "test" };
            TestPofile = new ProfileModel { LoginName = "test", Email = "test@test.com", IsActive = true };
            TestTest = new TestModel
                            { Author = TestPofile, CreationDate = new DateTime(2010, 1, 1), Name = "test", TestType = TestTestType };
            LatestTest = new TestModel
                              { Author = TestPofile, CreationDate = new DateTime(2011, 1, 1), Name = "test", TestType = TestTestType };
            TestCourseType = new CourseTypeModel { TypeName = "Fizyka" };
            TestCourseType1 = new CourseTypeModel { TypeName = "Matematyka" };
            TestGroupType = new GroupTypeModel { TypeName = "test" };
            TestGroupType1 = new GroupTypeModel { TypeName = "test1" };
            TestGroup = new GroupModel { GroupType = TestGroupType, GroupName = "test" };
            TestForum = new ForumModel { Author = "test", Name = "test" };
            TestShoutBox = new ShoutboxModel();
            TestShoutBox.Messages.Add(TestShoutBoxMessage);
            TestShoutBox.Messages.Add(TestShoutBoxMessage1);
            TestSurvey = new SurveyModel
                              { SurveyText = "Smiga chodzi fruwa ?", DateCreated = new DateTime(2010, 1, 1) };
            TestLatestSurvey = new SurveyModel
                                    { SurveyText = "Smiga chodzi fruwa ?", DateCreated = new DateTime(2011, 1, 1) };


            TestCourse2 = new CourseModel
                               {
                CourseType = TestCourseType1,
                ShoutBox = TestShoutBox,
                Forum = TestForum,
                Group = TestGroup,
                CreationDate = DateTime.Now,
                Description = "test1",
                Logo = "/test1.jpg",
                Name = "test1",
                Password ="test"

            };

            TestCourse3 = new CourseModel
                               {
                CourseType = TestCourseType1,
                ShoutBox = TestShoutBox,
                Forum = TestForum,
                Group = TestGroup,
                CreationDate = DateTime.Now,
                Description = "test1",
                Logo = "/test1.jpg",
                Name = "test1",
                Password = null

            };
            TestContentType = new ContentTypeModel {TypeName = "test"};
            TestContent = new ContentModel
                               {
                                   ContentUrl = "test",
                                   CreationDate = DateTime.Now,
                                   DownloadNumber = 0,
                                   Name="test",
                                   Text = "test",
                                   Type = TestContentType
                               };
            TestJournalMark = new JournalMarkModel { Name = "Zaliczenie", Value = "5" };
            TestJournal = new JournalModel
                               { Course = TestCourse3,Marks = new List<JournalMarkModel>
                                                                                     { TestJournalMark }, Name = "test journal" };

            FileModel = new FileModel {FileName = "test", Address = "test"};
            SectionModel = new SectionModel {IconName = "test", Text = "test", Title = "tset"};

            LearningMaterialModel = new LearningMaterialModel
                                        {
                                            Level = 1,
                                            CreationDate = DateTime.Now,
                                            Description = "test",
                                            Goals = "test",
                                            Summary = "test",
                                            UpdateDate = DateTime.Now,
                                            VersionNumber = "111",
                                            Tests = new List<TestModel> {TestTest},
                                            Files = new List<FileModel> {FileModel},
                                            Sections = new List<SectionModel> {SectionModel}
                                        };
            TestCourse1 = new CourseModel
            {
                CourseType = TestCourseType,
                ShoutBox = TestShoutBox,
                Forum = TestForum,
                Group = TestGroup,
                CreationDate = DateTime.Now,
                Description = "test",
                Logo = "/test.jpg",
                Name = "test",
                Password = "test",
                LearningMaterials = new List<LearningMaterialModel> { LearningMaterialModel }
            };

            using (var session = DataAccess.OpenSession())
            {
                session.Save(TestPofile);
                session.Save(TestShoutBoxMessage);
                session.Save(TestShoutBoxMessage1);
                session.Save(TestCourseType);
                session.Save(TestGroupType);
                session.Save(TestGroupType1);
                session.Save(TestGroup);
                session.Save(TestForum);
                session.Save(TestShoutBox);
                session.Save(TestCourseType1);
                session.Save(TestSurvey);
                session.Save(TestLatestSurvey);
                session.Save(TestTestType);
                session.Save(TestTest);
                session.Save(LatestTest);
                session.Save(TestCourse1);
                session.Save(TestCourse2);
                session.Save(TestQuestionAnswer);
                session.Save(TestQuestion);
                session.Save(TestCourse3);
                session.Save(TestJournalMark);
                session.Save(TestJournal);
                session.Save(TestContentType);
                session.Save(TestContent);
                session.Save(FileModel);
                session.Save(SectionModel);
                session.Save(LearningMaterialModel);
                session.Flush();
            }
        }

    }
}
