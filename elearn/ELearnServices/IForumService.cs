﻿using System.ServiceModel;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IForumService" in both code and config file together.
    [ServiceContract]
    public interface IForumService
    {
        [OperationContract]
        void DoWork();
    }
}
