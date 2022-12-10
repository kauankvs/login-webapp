using LoginWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginWebApp.Context
{
    public class LoginContext : DbContext
    {
        public LoginContext(DbContextOptions<LoginContext> options) : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Usuario>().ToTable("Usuario");
        }
    }
}
