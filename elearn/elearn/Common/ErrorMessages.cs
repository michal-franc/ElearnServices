using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace elearn.Common.ErrorMessages
{
    public static class Profile
    {
        public static string SetAsInactiveFailed
        {
            get
            {
                return "Set as inactive failed!";
            }
        }

        public static string ProfileUpdateFail
        {
            get
            {
                return "Problem Updating Profile";
            }
        }

        public static string RoleUpdateFail
        {
            get
            {
                return "Problem Updating Role";
            }
        }
    }
}