using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;
using NHiberanteDal.DataAccess;
using NHiberanteDal.Models;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "JournalService" in code, svc and config file together.
    public class JournalService : IJournalService
    {
        public JournalModelDto GetJournalDetails(int id)
        {
            JournalModelDto modelDto = null;
            using (var session = DataAccess.OpenSession())
            {
                 var model = session.Get<JournalModel>(id);
                 modelDto = JournalModelDto.Map(model);
            }
            return modelDto;
        }
    }
}
