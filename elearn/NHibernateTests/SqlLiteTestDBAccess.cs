using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHiberanteDal.DataAccess;

//Based on Maciej Aniserowicz Samples  http://www.maciejaniserowicz.com/

namespace NHibernateTests
{
    public class SqlLiteTestDBAccess
    {
        private static Configuration _configuration;
        private static ISessionFactory _sessionFactory;
        private readonly ISession _session;

        public SqlLiteTestDBAccess()
        {
            // configure NHibernate once for for all tests
            EnsureConfigured();

            // open one session for each test
            _session = _sessionFactory.OpenSession();

            // recreate the whole test database for each unit test
            RecreateDataBase();

            // replace default session creation logic to return the newly opened session meant to be used throughout the whole test;
            DataAccess.OpenSession = () =>
                                            {
                                                Console.WriteLine("----------------------------");
                                                Console.WriteLine("New session...");
                                                Console.WriteLine("----------------------------");

                                                _session.Clear();
                                                return new UndisposableSession(_session);
                                            };

        }

        private void RecreateDataBase()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Creating database schema...");
            Console.WriteLine("----------------------------");

            // first param to TRUE if output to console needed; otherwise - FALSE
            new SchemaExport(_configuration).Execute(true, true, false, _session.Connection, null);

            Console.WriteLine("----------------------------");
            Console.WriteLine("Schema created.");
            Console.WriteLine("----------------------------");
        }

        private static readonly object _syncRoot = new object();
        private void EnsureConfigured()
        {
            if (_configuration == null)
            {
                lock (_syncRoot)
                {
                    if (_configuration == null)
                    {
                        _configuration = Fluently.Configure()
                            .Database(() => SQLiteConfiguration.Standard.InMemory().ShowSql())
                            .Mappings(x => x.FluentMappings.AddFromAssembly(typeof(Models.ProfileModel).Assembly))
                            .BuildConfiguration();

                        _sessionFactory = _configuration.BuildSessionFactory();
                    }
                }
            }
        }

        public void Dispose()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("Disposing session...");
            Console.WriteLine("----------------------------");
            _session.Dispose();
        }
    }
}
