using System;
using System.Collections.Generic;
using System.Text;
using BooksFrontend.Models;
using System.Linq;


namespace BooksFrontend.Services
{
    public class CRUD : ICRUD
    {
        private BooksContext _context;
        public CRUD(BooksContext context)
        {
            _context = context;
        }
        public void Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
           _context.Books.Remove(book);
            _context.SaveChanges(); 
        }

        public List<Book> GetBook()
        {
            return _context.Books.ToList(); 
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }
}
