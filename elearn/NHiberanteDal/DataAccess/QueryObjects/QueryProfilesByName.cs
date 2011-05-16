using System;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    public class QueryProfilesByName : IQueryObject
    {
        private readonly string _value;

        public QueryProfilesByName(string userName)
        {
            _value = userName;
        }

        public string Query
        {
            get  {return String.Format
                ("from ProfileModel p where p.Name = '{0}'", _value);}
        }
    }
}
