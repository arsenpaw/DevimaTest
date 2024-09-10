
using Microsoft.EntityFrameworkCore;
using StarWarsWebApi.Models;


namespace StarWarsWebApi.Context
{
    public class StarWarsContext : DbContext
    {
       
        public DbSet<PersonDbModel> Persons { get; set; }

        public StarWarsContext(DbContextOptions<StarWarsContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<PersonDbModel>()
                .HasIndex(p => p.Name)
                .IsUnique();
            modelBuilder.Entity<PersonDbModel>()
                .HasIndex(p => p.ExternalApiId)
                .IsUnique();
            modelBuilder.Entity<PersonDbModel>()
                .HasIndex(p => p.PrivateId)
                .IsUnique();    
        }



    }
}
