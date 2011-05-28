using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JournalService" in code, svc and config file together.
    public class JournalService : IJournalService
    {
        public JournalService()
        {
            DtoMappings.Initialize();
        }

        public JournalModelDto GetJournalDetails(int id)
        {
            JournalModelDto modelDto;
            using (var session = DataAccess.OpenSession())
            {
                 var model = session.Get<JournalModel>(id);
                 modelDto = JournalModelDto.Map(model);
            }
            return modelDto;
        }

        public bool AddMark(int journalId, JournalMarkModelDto markDto)
        {
            using (var session = DataAccess.OpenSession())
            {
                var model = session.Get<JournalModel>(journalId);
                model.Marks.Add(JournalMarkModelDto.UnMap(markDto));
            }
            return true;
        }
    }
}
