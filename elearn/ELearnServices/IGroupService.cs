using System.Collections.Generic;
using System.ServiceModel;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGroupService" in both code and config file together.
    [ServiceContract]
    public interface IGroupService
    {
        [OperationContract]
        List<GroupModelDto> GetAllGroups();

        [OperationContract]
        GroupModelDto GetGroup(int id);

        [OperationContract]
        int AddGroup(GroupModelDto groupModelDto);

        [OperationContract]
        bool DeleteGroup(GroupModelDto groupDto);

        [OperationContract]
        bool UpdateGroup(GroupModelDto groupModelDto);

        [OperationContract]
        IList<GroupTypeModelDto> GetGroupTypes();

        [OperationContract]
        IList<GroupTypeModelDto> GetGroupTypeByName(string typeName);

        [OperationContract]
        bool AddProfileToGroup(int groupId,int profileId);

        [OperationContract]
        bool RemoveProfileFromGroup(int groupId, int profileId);
    }
}
