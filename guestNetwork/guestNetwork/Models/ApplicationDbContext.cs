using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNet.Identity.EntityFramework;

namespace guestNetwork.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            System.Diagnostics.Debug.WriteLine("applicationContextCreated");
        }
        public IDbSet<Advertisement> Advertisements { get; set; }
        public IDbSet<Language> Languages { get; set; }
        public IDbSet<Response> Responses { get; set; }


        public void MarkChanged(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}