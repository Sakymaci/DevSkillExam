using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookCrudOperationASP_Net.Data
{
    public class BookRepository : IBooksRepository
    {
        private readonly BookDbContext _entities;

        public BookRepository(BookDbContext bookDbContext)
        {
            this._entities = bookDbContext;
        }

        public List<Book> GetAllBooks()
        {
            return _entities.Books.ToList(); // Retrieves all books from the database
        }


        public Book GetBookById(int id)
        {
            return _entities.Books.Find(id); // Find and return the book by id or return null if not found
        }

        public bool AddBook(Book book)
        {
            if (book == null || _entities.Books.Any(b => b.isbn == book.isbn))
            {
                return false; // Return false if the book is null or if a book with the same ISBN already exists
            }

            _entities.Books.Add(book);
            _entities.SaveChanges(); // Save changes to the database
        return true;
        }

        public bool UpdateBook(Book book)
        {
        if (book == null || _entities.Books.Any(b => b.isbn == book.isbn && b.Id != book.Id))
        {
            return false; // Return false if the book is null or if an existing book has the same ISBN but a different ID
        }

        _entities.Books.Update(book);
        _entities.SaveChanges(); // Save changes to the database
        return true;
        }


        public bool DeleteBook(int id)
        {
        var book = _entities.Books.Find(id);
        if (book == null)
        {
            return false; // Return false if no book with the given ID is found
        }

        _entities.Books.Remove(book);
        _entities.SaveChanges(); // Save changes to the database
        return true;
        }

    }
}