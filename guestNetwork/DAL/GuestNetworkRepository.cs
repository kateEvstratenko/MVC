using System;
using System.Data.Entity;
using guestNetwork.Models;
using System.Linq;

namespace guestNetwork
{
    public abstract class GuestNetworkRepository<T> : IGuestNetworkRepository<T> where T: class
    {
        internal IApplicationDbContext Context;
        internal IDbSet<T> DbSet;
        protected GuestNetworkRepository(IApplicationDbContext context, Func<IApplicationDbContext, IDbSet<T>> dbSetSelector)
        {
            Context = context;
            DbSet = dbSetSelector(context);
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
            Context.MarkChanged(entity);
        }

        public T Get(int id)
        {
            var result = DbSet.Find(id);
            if (result != null)
            {
                return result;
            }
            throw new NullReferenceException();
        }

        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

       public void Save()
       {
           Context.SaveChanges();
       }
    }
}