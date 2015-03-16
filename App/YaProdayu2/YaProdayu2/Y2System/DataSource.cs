using System.Collections.Generic;

namespace YaProdayu2.Y2System
{
    public class DataService<T> where T : class
    {
        public void Save(T obj)
        {
            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(obj);
                    transaction.Commit();
                }
            }
        }

        public void Upsert(T obj)
        {
            using (var session = DBHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(obj);
                    transaction.Commit();
                }
            }
        }

        public T Get(int id)
        {
            T item = null;

            using (var session = DBHelper.OpenSession())
            {
                item = session.Get<T>(id);
            }

            return item;
        }

        public IList<T> GetAll()
        {
            IList<T> list = null;

            using (var session = DBHelper.OpenSession())
            {
                list = session.CreateCriteria<T>().List<T>();
            }

            return list;
        }

        public void Delete(int id)
        {
            using (var session = DBHelper.OpenSession())
            {
                session.Delete(id);
            }
        }
    }
}