using System;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    public class QueryCoursesByProfileId : IQueryObject
    {
        private readonly string _value;

        public QueryCoursesByProfileId(string userName)
        {
            _value = userName;
        }

        public string Query
        {
            get
            {
                throw new NotImplementedException();
                return 
                    String.Format
                    ("from ProfileModel p where p.Name = '{0}'", _value);
            }
        }
    }
}