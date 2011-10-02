using System;
using System.ServiceModel.Activation;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace ELearnServices
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class JournalService : IJournalService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public JournalService()
        {
            Logger.Info("Created JournalService");
            //HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
            DtoMappings.Initialize();
        }

        public JournalModelDto GetJournalDetails(int id)
        {
            try
            {
                JournalModelDto modelDto;
                using (var session = DataAccess.OpenSession())
                {
                    var model = session.Get<JournalModel>(id);
                    modelDto = JournalModelDto.Map(model);
                }
                return modelDto;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : JournalService.GetJournalDetails - {0}", ex.Message);
                return null;
            }
        }

        //todotest : test
        public bool CreateJournal(int courseId,int profileId)
        {
            try
            {
                var course = new Repository<CourseModel>().GetById(courseId);
                var profile = new Repository<ProfileModel>().GetById(profileId);
                var newJournal = new JournalModel {Name = String.Format("{0}", course.Name), Course = course,IsActive=true};
                profile.Journals.Add(newJournal);
                new Repository<ProfileModel>().Update(profile);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : CreateJournal - {0}", ex.Message);
                return false;
            }
        }

        public bool AddMark(int journalId, JournalMarkModelDto markDto)
        {
            try
            {
                Logger.Debug("AddMark - journal id : {0} , mark :  {1} ", journalId, markDto.Name);
                using(var session = DataAccess.OpenSession())
                {
                    var journalModel = session.Get<JournalModel>(journalId);
                    var mark = JournalMarkModelDto.UnMap(markDto);
                    journalModel.Marks.Add(mark);
                    session.Update(journalModel);
                }
                Logger.Debug("Journal : Mark Added");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : AddMark \r\n {0}", ex.Message);
                return false;
            }
        }


        public bool RemoveMark(int markId)
        {
            try
            {
                var repo = new Repository<JournalMarkModel>();
                var mark = repo.GetById(markId);
                repo.Remove(mark);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : JournalService.RemoveMark - {0}", ex.Message);
                return false;
            }
        }


        public int GetCourseIdForTest(int testId)
        {
            try
            {
                using (var session = DataAccess.OpenSession())
                {
                    var lmId =
                        session.CreateQuery(
                            String.Format("select lm.ID from LearningMaterialModel as lm , TestModel as test where test.ID ='{0}' and test in elements(lm.Tests)", testId)).UniqueResult<int>();
                    var courseID =
                        session.CreateQuery(
                            String.Format(
                                "select course.ID from CourseModel as course , LearningMaterialModel as lm where lm.ID ='{0}'  and lm in elements(course.LearningMaterials)", lmId)).UniqueResult<int>();
                    session.Flush();
                    return courseID;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error : GetCourseIdForTest \r\n {0}", ex.Message);
                return -1;
            }
        }
    }
}
