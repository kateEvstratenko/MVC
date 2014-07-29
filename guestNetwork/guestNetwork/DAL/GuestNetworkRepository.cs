using guestNetwork.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace guestNetwork.DAL
{
    public class GuestNetworkRepository<T> : IGuestNetworkRepository<T> where T:class
    {
        internal ApplicationDbContext Context;
        internal DbSet<T> DbSet;

        public GuestNetworkRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
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
            Context.Entry(entity).State = EntityState.Modified;
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

       public void Save()
       {
           Context.SaveChanges();
       }
    }
}