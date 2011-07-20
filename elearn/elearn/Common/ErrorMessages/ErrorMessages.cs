using System;

namespace elearn.Common.ErrorMessages
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

        public static string NoProfile
        {
            get { return "Error - No Profile found"; }
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
            get { return "Update course model error -  validation"; }
        }

        public static string DeleteError
        {
            get { return "Problem Deleting Course"; }
        }

        public static string UpdateToDbError
        {
            get { return "Problem Updating Course in DB"; }
        }

        public static string GroupTypeError
        {
            get { return "No course group type error."; }
        }
    }

    public static class Group
    {
        public static string ProfileJoinError
        {
            get { return "Null Profile"; }
        }

        public static string ProfileLeaveError
        {
            get { return "Null Profile"; }
        }
    }

    public static class Test
    {
        public static string TestIdError
        {
            get { return "Error - Wrong test Id Number."; }
        }
    }

    public static class Journal
    {
        public static string NoJournals
        {
            get { return "Error - No Journals found"; }
        }

        public static string NoProfile
        {
            get { return "Error - No Profile found";}
        }
    }
}