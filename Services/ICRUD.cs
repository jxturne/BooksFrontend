using System;
using System.Collections.Generic;
using System.Text;
using BooksFrontend.Models;

namespace BooksFrontend.Services
{
    public interface ICRUD
    {
        void Create(Book book);
        List<Book> GetBook();
        void Update(Book book);
        void Delete(Book book);
    }
}
