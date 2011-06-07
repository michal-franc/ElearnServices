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
                using (var session = DataAccess.OpenSession())
                {
                    var journalModel = session.Get<JournalModel>(journalId);
                    journalModel.Marks.Add(JournalMarkModelDto.UnMap(markDto));
                    session.Save(journalModel);
                }
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
                new Repository<JournalMarkModel>().Remove(new JournalMarkModel(){ID=markId});
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
