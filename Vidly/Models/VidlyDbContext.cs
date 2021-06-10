using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace Vidly.Models
{
    public class VidlyDbContext : DbContext
    {
        public VidlyDbContext() : base("MyCon")
        { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(c => c.DateOfBirth)
                .HasColumnType("Date");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
    
}