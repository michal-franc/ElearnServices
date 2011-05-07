using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ELearnServices
{
    public class MembershipRoleProvider : IRoleProvider
    {
        public string[] GetAllRoles()
        {
            return Roles.GetAllRoles();
        }

        public bool IsUserInRole(string userName, string role)
        {
            return Roles.IsUserInRole(userName, role);
        }

        public void CreateRole(string roleName)
        {
            Roles.CreateRole(roleName);
        }

        public string[] GetRolesForUser(string userName)
        {
            return Roles.GetRolesForUser(userName);
        }

        public bool RoleExists(string roleName)
        {
            return Roles.RoleExists(roleName);
        }

        public void AddUserToRole(string userName, string roleName)
        {
            Roles.AddUserToRole(userName, roleName);
        }

        public void RemoveUserFromRole(string userName,string roleName)
        {
            Roles.RemoveUserFromRole(userName, roleName);
        }      
    }
}