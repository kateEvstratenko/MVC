using guestNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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