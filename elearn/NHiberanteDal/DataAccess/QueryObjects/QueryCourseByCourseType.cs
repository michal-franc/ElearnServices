using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHiberanteDal.Models;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    public class QueryCourseByCourseType : IQueryObject
    {
        private CourseTypeModel _testCourseType;

        public QueryCourseByCourseType(CourseTypeModel _testCourseType)
        {
            // TODO: Complete member initialization
            this._testCourseType = _testCourseType;
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
