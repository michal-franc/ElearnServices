using System.ServiceModel;
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
        bool RemoveMark(int markId);

        [OperationContract]
        JournalModelDto GetJournalDetails(int id);

        [OperationContract]
        bool CreateJournal(int courseId, int profileId);

        [OperationContract]
        int GetCourseIdForTest(int testId);
    }
}
