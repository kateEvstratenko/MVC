using System.Data.Entity;

namespace guestNetwork.Models
{
    public interface IApplicationDbContext
    {
        DbSet<Advertisement> Advertisements { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Response> Responses { get; set; }

        void SaveChanges();
    }
}
