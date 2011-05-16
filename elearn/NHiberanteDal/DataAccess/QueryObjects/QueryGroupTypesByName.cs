using System;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    public class QueryGroupTypesByName : IQueryObject
    {
        private readonly string _value;
        public QueryGroupTypesByName(string value)
        {
            _value = value;
        }

        public string Query
        {
            get { return String.Format
                ("from GroupTypeModel c where c.TypeName = '{0}'", _value);
            }
        }
    }
}
