using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mon.Models;

namespace Mon.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUsuario> /* DbContext*/
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<AppUsuario> AppUsuario   { get; set; }

    }
}
