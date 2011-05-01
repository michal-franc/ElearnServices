using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ELearnServices
{
    public interface IRoleProvider
    {
        string[] GetAllRoles();
        bool IsUserInRole(string userName, string role);
        void CreateRole(string roleName);
        string[] GetRolesForUser(string userName);
        bool RoleExists(string roleName);
        void AddUserToRole(string userName,string roleName);
        void RemoveUserFromRole(string userName,string roleName);
    }
}
