using Microsoft.EntityFrameworkCore;
using PetLar.Models;

namespace PetLar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder){
            builder.Entity<User>().ToTable("users");
            builder.Entity<Animal>().ToTable("Animals");
        }
    }
}
