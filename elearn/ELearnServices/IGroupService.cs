﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
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
    }
}
