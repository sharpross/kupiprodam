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
        public IList<Seller> GetAll()
        {
            IList<Seller> Sellers;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                Sellers = session.CreateQuery("from " + typeof(Seller)).List<Seller>();;
            }
            return Sellers;
        }

        public Seller GetById(int Id)
        {
            Seller seller = new Seller();
            using (ISession session = NHibernateHelper.OpenSession())
            {
                seller = session.Get<Seller>(Id);
            }
            return seller;
        }

        public int Create(Seller seller)
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

        public void Update(Seller seller)
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

        public void Delete(Seller seller) 
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
