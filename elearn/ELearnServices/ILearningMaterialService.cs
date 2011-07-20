using System.ServiceModel;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    [ServiceContract]
    public interface ILearningMaterialService
    {
        [OperationContract]
        LearningMaterialDto GetById(int id);

        [OperationContract]
        bool Update(LearningMaterialDto learningMaterial);

        [OperationContract]
        int? Add(LearningMaterialDto learningMaterial);
    }
}
