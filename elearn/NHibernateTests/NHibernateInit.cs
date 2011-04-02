using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Models;

namespace NHibernateTests
{
    [TestFixture]
    public class NHibernateInit
    {

        [TestCase]
        public void CanGenerateMappings()
        {
            NHiberanteDal.SessionFactory.ResetSchema();
        }

        //Test the default value creation of CreationDate DateTime

        [Test]
        public void CanFillWithTestData()
        {
            //2 ContentTypeModel
            ContentTypeModel contentTypeImage = new ContentTypeModel() { TypeName="Image" };
            ContentTypeModel contentTypeVideo = new ContentTypeModel() { TypeName = "Video" };

            //2 Group Model
            //2 Group ModelTypes

            //2 Forum Model
            //2 Topic Model
            //2 Post Model

          
            //2 Profile Model
            //2 Profile ModelTypes

            //2 ContentModel
            ContentModel contentImage = new ContentModel() { ContentUrl="/image/logo.jpeg" , Name="LogoImage", Type=contentTypeImage };
            ContentModel contentBideo = new ContentModel() { ContentUrl="/video/intro.avi" , Name="IntroVideo", Type=contentTypeVideo };

            //2 Course Type Model
            CourseTypeModel courseTypeMath = new CourseTypeModel() { TypeName="Math" };
            CourseTypeModel courseTypeProgramming = new CourseTypeModel() { TypeName = "Programming" };

            //2 Course Model
            //CourseModel courseMath = new CourseModel() { CourseType=courseTypeMath , Forum= , Group=  };


            //2 Survey Model
            //4 Survey Question Model

            //2 Private Message Model

            //2 Journal Model
            //4 Journal Mark

            //1 ShoutBoxModel
            //2 ShoutBoxMessages

            //2 TestModel
            //2 TestType
            //2 TestQuestionModel
            //2 QuestionAnswerModel
        }

    }
}
