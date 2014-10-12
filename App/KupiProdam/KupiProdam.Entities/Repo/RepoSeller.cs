using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using KupiProdam.Entities.Entites;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace KupiProdam.Entities
{
    public class RepoSeller
    {
        public IList<User> GetAll()
        {
            IList<User> Sellers;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Sellers = session.CreateQuery("from " + typeof(User)).List<User>();;
            }
            return Sellers;
        }

        public User GetById(int Id)
        {
            User seller = new User();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                seller = session.Get<User>(Id);
            }
            return seller;
        }

        public int Create(User seller)
        {
            int sellNom = 0;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                //Perform transaction 
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Save(seller);
                    tran.Commit();
                }
            }
            return sellNom;
        }

        public void Update(User seller)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction tran = session.BeginTransaction())
                {
                    session.Update(seller);
                    tran.Commit();
                }
            }
        }

        public void Delete(User seller) 
        {
            using (ISession session = NHibernateHelper.OpenSession()) 
            { 
                using (ITransaction tran = session.BeginTransaction()) 
                { 
                    session.Delete(seller); 
                    tran.Commit(); 
                } 
            } 
        } 
    }
}
