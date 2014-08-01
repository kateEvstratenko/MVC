using System.Data.Entity;
using System.Linq;
using guestNetwork;

namespace DAL
{
    public class GuestNetworkRepository<T> : IGuestNetworkRepository<T> where T : class
    {
        internal IDbSet<T> DbSet;
        public GuestNetworkRepository(IDbSet<T> dbSet)
        {
            DbSet = dbSet;
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = DbSet.Find(id);
            DbSet.Attach(entity);
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
        }

        public T Get(int id)
        {
            var result = DbSet.Find(id);
            
            return result;
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }
    }
}