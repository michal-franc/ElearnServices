using System;
using NHiberanteDal.Models;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    public class QueryCourseByCourseType : IQueryObject
    {
        private readonly CourseTypeModel _testCourseType;

        public QueryCourseByCourseType(CourseTypeModel testCourseType)
        {
            // TODO: Complete member initialization
            _testCourseType = testCourseType;
        }

        public string Query
        {
            get
            {
                return String.Format
                    ("from CourseModel c where c.CourseType.TypeName = '{0}'", _testCourseType.TypeName);
            }
        }
    }
}
