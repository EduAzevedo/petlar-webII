using Microsoft.EntityFrameworkCore;
using PetLar.Models;
using PetLar.Models.OngModels;

namespace PetLar.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ong> Ongs { get; set; }
        public DbSet<Animal> Animals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento 1 para 1 entre User e Ong
            modelBuilder.Entity<User>()
                .HasOne(u => u.Ong)
                .WithOne(o => o.User)
                .HasForeignKey<Ong>(o => o.UserId);

            // Relacionamento 1 para muitos entre Ong e Animal
            modelBuilder.Entity<Ong>()
                .HasMany(o => o.Animals)
                .WithOne(a => a.Ong)
                .HasForeignKey(a => a.OngId);
        }
    }
}
