using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BooksAPI.Models
{
    public class BooksDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Retrieve connection string from your configuration
                string connectionString = ConfigurationManager.ConnectionStrings["dbConnect"].ConnectionString;

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Category>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)  // Book requires a Category
            .WithMany(c => c.Books)        // Category has many Books
            .HasForeignKey(b => b.CategoryId);
        }
    }
}