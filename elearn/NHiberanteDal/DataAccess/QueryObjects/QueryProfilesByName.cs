using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    public class QueryProfilesByName : IQueryObject
    {
        private string _value;

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
