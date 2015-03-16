using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YaProdayu2.Models.Entities;

namespace YaProdayu2.Y2System
{
    public class DBHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                //.Database(MySQLConfiguration.Standard.ConnectionString(@"CharSet=utf8; Database=kupiprodam; Data Source=localhost; User Id=admin;Password=admin;").ShowSql())
                .Database(MySQLConfiguration.Standard.ConnectionString(@"CharSet=utf8; Database=kupiprodam; Data Source=62.109.31.30; Port=3306; User Id=admin;Password=admin;").ShowSql())
                //.Database(MySQLConfiguration.Standard.ConnectionString(@"CharSet=utf8; Database=kupiprodam; Data Source=localhost; User Id=root;Password=cnhflbdfhb2;").ShowSql())
                .Mappings(m =>
                    m.FluentMappings
                    .AddFromAssemblyOf<UserSystem>()
                    .AddFromAssemblyOf<Tender>()
                    .AddFromAssemblyOf<TenderMessage>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(false, false))
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}