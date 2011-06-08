using System;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace ELearnServices
{
    public class JournalService : IJournalService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        public JournalService()
        {
            Logger.Info("Created JournalService");
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

        public bool AddMark(int journalId, JournalMarkModelDto markDto)
        {
            try
            {
                Logger.Debug("Journal Repository : AddMark - journal id : {0} , mark :  {1} ", journalId, markDto.ToString());
                
                var journalModel = new Repository<JournalModel>().GetById(journalId);
                var mark = JournalMarkModelDto.UnMap(markDto);
                journalModel.Marks.Add(mark);
                new Repository<JournalModel>().Update(journalModel);
                
                Logger.Debug("Journal Repository : Marked Added");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : JournalService.AddMark - {0}", ex.Message);
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
    }
}
