using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace guestNetwork.Models
{
    // You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser<int, CustomUserLogin, CustomUserRole, CustomUserClaim> 
    {
        public  string Firstname { get; set; }
        public  string Surname { get; set; }
        public  string Country { get; set; }
        public  string City { get; set; }
        //public  string Email { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, int> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(
                this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        } 
    }

    public class CustomRole : IdentityRole<int, CustomUserRole>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
    public class CustomUserRole : IdentityUserRole<int> { }
    public class CustomUserClaim : IdentityUserClaim<int> { }
    public class CustomUserLogin : IdentityUserLogin<int> { }


    public class Language
    {
        public  int Id { get; set; }
        public  string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }

    public enum Type
    {
        Invite,
        Find
    }

    public class Advertisement
    {
        public long Id { get; set; }
        public string UserId { get; set; }

        //public long UserResponseId { }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual Type Type { get; set; }
        public virtual User User { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}