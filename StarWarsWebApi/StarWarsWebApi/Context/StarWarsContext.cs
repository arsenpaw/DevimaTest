using Microsoft.EntityFrameworkCore;
using StarWarsWebApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication.Context
{
    public class StarWarsContext : DbContext
    {
       
        public DbSet<PersonDbModel> Persons { get; set; }

        public StarWarsContext(DbContextOptions<StarWarsContext> options) : base(options)
        {
            Database.EnsureCreated();

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
        }



    }
}
