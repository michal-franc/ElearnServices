using System;
using System.Web.Configuration;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Cfg;

//Based on Maciej Aniserowicz Samples http://www.maciejaniserowicz.com/

namespace NHiberanteDal.DataAccess
{
    public static class DataAccess
    {
        private  const string TestConnString =  "Data Source=.\\SQL2008;Initial Catalog=elearntest;Integrated Security=SSPI";


        private  static string ConnectionString
        {
            get
            {
                if (WebConfigurationManager.ConnectionStrings["DBConString"] != null)
                {
                    return WebConfigurationManager.ConnectionStrings["DBConString"].ConnectionString;
                }
                return TestConnString;
            }
        }
        private static ISessionFactory _sessionFactory;
        private static readonly object SyncRoot = new object();
        private static Configuration _configuration;

        private static readonly Func<ISession> DefaultOpenSession = () =>
        {
            if (_sessionFactory == null)
            {
                lock (SyncRoot)
                {
                    if (_sessionFactory == null)
                        Configure();
                }

            }

            return _sessionFactory.OpenSession();
        };

        private static void Configure()
        {
            _sessionFactory = Configuration.BuildSessionFactory();
        }

        public static  Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = Fluently.Configure().
                            Database(MsSqlConfiguration.MsSql2008.ConnectionString(ConnectionString))
                            .Mappings(x => x.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly())).BuildConfiguration();
                }
                return _configuration;
            }
            set
            {
                _configuration = value;
            }
        }


        [ThreadStatic]
        private static Func<ISession> _openSession;

        public static Func<ISession> OpenSession
        {
            set { _openSession = value; }
            get { return _openSession ?? DefaultOpenSession; }
        }

        public static void InTransaction(Action<ISession> operation)
        {
            using (var session = OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    operation(session);

                    tx.Commit();
               }
           }
        }

        public static void ResetDb()
        {
            new SchemaUpdate(Configuration).Execute(true, true);
        }

    }
}
