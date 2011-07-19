using ELearnServices;
using NUnit.Framework;

namespace NHibernateTests.ServicesTests
{
    [TestFixture]
    class LearningMaterialServiceTests : InMemoryWithSampleData
    {
        [Test]
        public void Can_get_learning_material_by_id()
        {
            #region Arrange
            #endregion

            #region Act

            var learningMaterial = new LearningMaterialService().GetById(1);

            #endregion

            #region Assert

            Assert.That(learningMaterial, Is.Not.Null);
            Assert.That(learningMaterial.ID, Is.EqualTo(1));
            #endregion
        }

        [Test]
        public void Can_update_learning_material()
        {
            #region Arrange
            var testData = "modified summary";
            #endregion

            #region Act
            var learningMaterial = new LearningMaterialService().GetById(1);

            learningMaterial.Summary = testData;
            new LearningMaterialService().Update(learningMaterial);

            learningMaterial = new LearningMaterialService().GetById(1);
            #endregion

            #region Assert

            Assert.That(learningMaterial, Is.Not.Null);
            Assert.That(learningMaterial.ID, Is.EqualTo(1));
            Assert.That(learningMaterial.Summary, Is.EqualTo(testData));
            #endregion
        }
    }
}
