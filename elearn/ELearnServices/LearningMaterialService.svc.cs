using System;
using System.ServiceModel.Activation;
using NHiberanteDal.DataAccess;
using NHiberanteDal.DTO;
using NHiberanteDal.Models;

namespace ELearnServices
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LearningMaterialService : ILearningMaterialService
    {
        private static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public LearningMaterialService()
        {
            Logger.Info("Created LearningMaterialService");
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            DtoMappings.Initialize();
        }

        public LearningMaterialDto GetById(int id)
        {
            try
            {
                using (var session = DataAccess.OpenSession())
                {
                    var learningMaterial = session.Get<LearningMaterialModel>(id);
                    return LearningMaterialDto.Map(learningMaterial);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error : LearningMaterialService.GetById -  \r\n {0}", ex.Message);
                return null;    
            }
        }

        public bool Update(LearningMaterialDto learningMaterial)
        {
            try
            {
                var learningModel = LearningMaterialDto.UnMap(learningMaterial);
                new Repository<LearningMaterialModel>().Update(learningModel);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : LearningMaterialService.Update -  \r\n {0}", ex.Message);
                return false;
            }
        }

        public bool UpdateWithouTests(LearningMaterialDto learningMaterial)
        {
            try
            {
                var learningModel = LearningMaterialDto.UnMap(learningMaterial);
                new Repository<LearningMaterialModel>().Update(learningModel);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : LearningMaterialService.Update -  \r\n {0}", ex.Message);
                return false;
            }
        }


        public int? Add(LearningMaterialDto learningMaterial)
        {
            try
            {
                var learningModel = LearningMaterialDto.UnMap(learningMaterial);
                return new Repository<LearningMaterialModel>().Add(learningModel);
            }
            catch (Exception ex)
            {
                Logger.Error("Error : LearningMaterialService.Update -  \r\n {0}", ex.Message);
                return null;
            }
        }
    }
}
