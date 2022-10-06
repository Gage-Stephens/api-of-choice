using APIofChoiceGage.Models;
using Microsoft.EntityFrameworkCore;

namespace APIofChoiceGage
{
    public class CharacterContext : DbContext
    {
        public DbSet<Character>? Characters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CharacterAPI;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString).UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(
                new Character() { Id = 1, Name = "Olfric", Strength = 50, Health = 50, Race = "Nord"},
                new Character() { Id = 2, Name = "Cicero", Strength = 50, Health = 50, Race = "RedGuard"});

        }
    }
}
