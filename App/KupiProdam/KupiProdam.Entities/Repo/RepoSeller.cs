using KupiProdam.Entities.Entites;
using NHibernate;
using NHibernate.Cfg;
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
        ISessionFactory sessionFactory;

        ISession OpenSession()
        {
            if (sessionFactory == null)
            {
                var cgf = new Configuration();
                var data = cgf.Configure(
                    HttpContext.Current.Server.MapPath(@"Models\Config\hibernate.cfg.xml")
                        .Replace("\\Account", string.Empty)
                        .Replace("\\Seller\\Card", string.Empty)
                        .Replace("\\Seller\\Index", string.Empty)
                        .Replace("\\Buyer", string.Empty)
                        .Replace("\\Home", string.Empty)
                        .Replace("\\Image", string.Empty)
                    );

                cgf.AddDirectory(new System.IO.DirectoryInfo(
                    HttpContext.Current.Server.MapPath(@"Models\Mappings")
                        .Replace("\\Account", string.Empty)
                        .Replace("\\Seller\\Card", string.Empty)
                        .Replace("\\Seller\\Index", string.Empty)
                        .Replace("\\Buyer", string.Empty)
                        .Replace("\\Home", string.Empty)
                        .Replace("\\Image", string.Empty)
                    ));

                sessionFactory = data.BuildSessionFactory();
            }
            return sessionFactory.OpenSession();
        }

        public IList<Seller> GetAll()
        {
            IList<Seller> Sellers;
            using (ISession session = OpenSession())
            {
                IQuery query = session.CreateQuery("from seller");
                Sellers = query.List<Seller>();
            }
            return Sellers;
        }

        public Seller GetById(int Id)
        {
            Seller seller = new Seller();
            using (ISession session = OpenSession())
            {
                seller = session.Get<Seller>(Id);
            }
            return seller;
        }

        public int Create(Seller seller)
        {
            int sellNom = 0;
            using (ISession session = OpenSession())
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
            using (ISession session = OpenSession())
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
            using (ISession session = OpenSession()) 
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
