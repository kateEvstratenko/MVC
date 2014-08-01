using System;
using System.Linq;
using System.Linq.Expressions;

namespace guestNetwork
{
    public interface IGuestNetworkRepository<T> where T : class
    {
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        T Get(int id);
        IQueryable<T> GetAll();
        void Save();
    }
}