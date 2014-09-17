using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;

namespace KupiProdam.Entities
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory 
        { 
            get 
            {
                if (_sessionFactory == null)
                {
                    InitializeSessionFactory();
                }
                return _sessionFactory;
            } 
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(
                        @"Server=localhost;Database=kupiprodam;Uid=root;Pwd=cnhflbdfhb2;"
                    ).ShowSql()
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<KupiProdam.Entities.Mapping.SellerMap>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
        }
    }
}
