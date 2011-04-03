﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;

//Based on Maciej Aniserowicz Samples http://www.maciejaniserowicz.com/

namespace NHiberanteDal.DataAccess
{
    public static class DataAccess
    {
        private static ISessionFactory _sessionFactory;
        private static readonly object _syncRoot = new object();

        private static readonly Func<ISession> _defaultOpenSession = () =>
        {
            if (_sessionFactory == null)
            {
                lock (_syncRoot)
                {
                    if (_sessionFactory == null)
                        Configure();
                }
            }

            return _sessionFactory.OpenSession();
        };

        private static void Configure()
        {
            _sessionFactory = Fluently.Configure().
                        Database(MsSqlConfiguration.MsSql2008.ConnectionString("Data Source=LaM-PC\\SQL2008;Initial Catalog=elearntest;Integrated Security=SSPI;"))
                        .Mappings(x => x.FluentMappings.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly()))
                        .BuildSessionFactory();
        }


        [ThreadStatic]
        private static Func<ISession> _openSession;

        public static Func<ISession> OpenSession
        {
            set { _openSession = value; }
            get { return _openSession ?? _defaultOpenSession; }
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
    }
}
