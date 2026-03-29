using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using BooksFrontend;


namespace BooksFrontend.Models
{
    public class BooksContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(GetBooks());
            base.OnModelCreating(modelBuilder);
        }
        private List<Book> GetBooks()
        {
            return new List<Book>
            {
                new Book { Isbn = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Description = "Classic novel set in the Jazz Age."},
                new Book { Isbn = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", Description = "Legendary novel addressing racial injustice."},
                new Book { Isbn = 3, Title = "1984", Author = "George Orwell", Description = "Dystopian novel about totalitarian regime."}
            };
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
