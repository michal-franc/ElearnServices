using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHiberanteDal.Mappings;

namespace NHiberanteDal.Repository
{
    public static class SessionFactory
    {
        public static ISession OpenSession()
        {
            return GetSessionFactory().OpenSession();
        }
        private static ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
            {
                _sessionFactory = CreateSessionFactory(UpdateSchema);
            }
            return _sessionFactory;
        }
        private static ISessionFactory _sessionFactory;

        public static void ResetSchema()
        {
            CreateSessionFactory(ResetSchema);
        }

        private static ISessionFactory CreateSessionFactory(Action<Configuration> func)
        {

            return Fluently.Configure().
                    Database(MsSqlConfiguration.MsSql2008.ConnectionString
                    ("Data Source=LaM-PC\\SQL2008;Initial Catalog=elearntest;Integrated Security=SSPI;"))
                    .Mappings(x => x.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()))
                    .ExposeConfiguration(func)
                    .BuildSessionFactory();
        }
        private static void UpdateSchema(Configuration config)
        {
            new SchemaUpdate(config).Execute(true, true);
        }

        public static void ResetSchema(Configuration config)
        {
            new SchemaExport(config).Create(true, true);
        }
    }
}
