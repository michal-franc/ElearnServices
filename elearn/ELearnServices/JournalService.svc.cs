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
                    var model = session.Get<JournalModel>(journalId);
                    model.Marks.Add(JournalMarkModelDto.UnMap(markDto));
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error : JournalService.AddMark - {0}", ex.Message);
                return false;
            }
        }
    }
}
