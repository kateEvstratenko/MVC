using guestNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace guestNetwork.DAL
{
    public class GuestNetworkRepository<T> : IGuestNetworkRepository<T> where T:class
    {
        internal ApplicationDbContext context;
        internal DbSet<T> dbSet;

        public GuestNetworkRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = dbSet.Find(id);
            dbSet.Attach(entity);
            dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList(); //???
        }

       public void Save()
       {
           context.SaveChanges();
       }
    }
}