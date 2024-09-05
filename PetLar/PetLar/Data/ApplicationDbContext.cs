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
    }
}
