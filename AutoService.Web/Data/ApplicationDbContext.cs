using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutoService.Web.Models;

namespace AutoService.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> // ✅ Заменяем IdentityUser на ApplicationUser
    {
        public DbSet<Request> Requests { get; set; } // Таблица заявок

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
