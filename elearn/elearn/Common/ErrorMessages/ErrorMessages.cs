﻿namespace elearn.Common.ErrorMessages
{
    public static class Profile
    {
        public static string SetAsInactiveFailed
        {
            get { return "Set as inactive failed!"; }
        }

        public static string ProfileUpdateFail
        {
            get { return "Problem Updating Profile"; }
        }

        public static string RoleUpdateFail
        {
            get { return "Problem Updating Role"; }
        }
    }

    public static class Course
    {
        public static string AddToDbError
        {
            get { return "Problem in DB while creating Course"; }
        }

        public static string ModelUpdateError
        {
            get { return "Update course model error"; }
        }

        public static string DeleteError
        {
            get { return "Problem Deleting Course"; }
        }

        public static string UpdateToDbError
        {
            get { return "Problem Updating Course in DB"; }
        }
    }
}