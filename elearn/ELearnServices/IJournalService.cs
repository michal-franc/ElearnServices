﻿using System.ServiceModel;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // : You can use the "Rename" command on the "Refactor" menu to change the interface name "IJournalService" in both code and config file together.
    [ServiceContract]
    public interface IJournalService
    {
        [OperationContract]
        bool AddMark(int journalId, JournalMarkModelDto markDto);

        [OperationContract]
        JournalModelDto GetJournalDetails(int id);
    }
}
