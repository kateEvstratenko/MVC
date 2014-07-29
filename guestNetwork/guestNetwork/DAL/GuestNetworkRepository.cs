using Antlr.Runtime.Misc;
using guestNetwork.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace guestNetwork.DAL
{
    public abstract class GuestNetworkRepository<T> : IGuestNetworkRepository<T> where T : class
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