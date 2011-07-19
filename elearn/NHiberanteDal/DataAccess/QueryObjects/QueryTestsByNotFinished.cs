using System;

namespace NHiberanteDal.DataAccess.QueryObjects
{
    class QueryTestsByNotFinished : IQueryObject
    {
        private int _testId;
        private int _profileId;

        public QueryTestsByNotFinished(int testId, int profileId)
        {
            _testId = testId;
            _profileId = profileId;
        }

        public string Query
        {
            get
            {
                return String.Format
                    ("from TestModel test where test.ID = '{0}'", _testId);
            }
        }
    }
}
