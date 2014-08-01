using System.Linq;

namespace DAL
{
    public interface IGuestNetworkRepository<T> where T : class
    {
        void Insert(T entity);
        void Delete(int id);
        void Update(T entity);
        T Get(int id);
        IQueryable<T> GetAll();
    }
}