using System.Data.Entity;
using Ninject.Activation;

namespace guestNetwork.Models
{
    public interface IApplicationDbContext
    {
        IDbSet<User> Users { get; set; }
        IDbSet<Advertisement> Advertisements { get; set; }
        IDbSet<Language> Languages { get; set; }
        IDbSet<Response> Responses { get; set; }

        void SaveChanges();
        void MarkChanged(object entity);
    }
}
