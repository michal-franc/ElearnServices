﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProfileService" in both code and config file together.
    [ServiceContract]
    public interface IProfileService
    {
        [OperationContract]
        int AddProfile(ProfileModelDto profile);

        [OperationContract]
        ProfileModelDto GetProfile(int id);

        [OperationContract]
        List<ProfileModelDto> GetAllProfiles();

        [OperationContract]
        bool DeleteProfile(ProfileModelDto profile);

        [OperationContract]
        bool UpdateProfile(ProfileModelDto profile);
    }
}
