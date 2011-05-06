﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using NHiberanteDal.DTO;
using System.Web.Security;
using NHiberanteDal.Models;

namespace ELearnServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProfileService" in both code and config file together.
    [ServiceContract]
    public interface IProfileService
    {
        [OperationContract]
        int AddProfile(ProfileModel profile);

        [OperationContract]
        ProfileModelDto GetProfile(int id);

        [OperationContract]
        List<ProfileModelDto> GetAllProfiles();

        [OperationContract]
        bool DeleteProfile(ProfileModelDto profile);

        [OperationContract]
        bool UpdateProfile(ProfileModelDto profile);

        [OperationContract]
        bool ValidateUser(string userName, string password);

        [OperationContract]
        void ResetPassword(string userName);

        [OperationContract]
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        [OperationContract]
        int GetMinPasswordLength();

        [OperationContract]
        MembershipCreateStatus CreateUser(string userName, string password, string email);

        [OperationContract]
        bool UpdateRole(ProfileModelDto profile, bool createIfNotExist);

        [OperationContract]
        ProfileModelDto GetByName(string userName);

        [OperationContract]
        bool IsUserInRoles(string userName, string[] roles);

        [OperationContract]
        string[] GetAllRoles();

        [OperationContract]
        bool AddRole(string roleName);

        [OperationContract]
        bool SetAsInactive(int id);

        [OperationContract]
        bool SetAsInactiveByName(string userName);
    }
}
