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
        public virtual ICollection<Language> Languages { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
        public virtual ICollection<Response> Responses { get; set; } 

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
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string mainImagePath { get; set; }
        public string Content { get; set; }
        public virtual Type Type { get; set; }
        public virtual User User { get; set; }
        public virtual Response Response { get; set; }
    }

    public class Response
    {
        [Key, ForeignKey("Advertisement")]
        public int AdvertisementId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Advertisement Advertisement{ get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<User, CustomRole, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Response> Responses { get; set; }
    }
}