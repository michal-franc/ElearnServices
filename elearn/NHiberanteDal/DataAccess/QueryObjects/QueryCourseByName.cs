using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    public class QueryCourseByName : IQueryObject
    {
        private string _value;
        public QueryCourseByName(string value)
        {
            _value = value;
        }

        public string Query
        {
            get { return String.Format
                ("from CourseModel c where c.Name = '{0}'", _value);}
        }
    }
}
