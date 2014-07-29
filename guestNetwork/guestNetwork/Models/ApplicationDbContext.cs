using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace guestNetwork.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Response> Responses { get; set; }



        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}