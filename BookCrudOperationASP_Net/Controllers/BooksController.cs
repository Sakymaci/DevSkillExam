using System;
using Microsoft.AspNetCore.Mvc;
using BookCrudOperationASP_Net.Models;
using BookCrudOperationASP_Net.Data;

namespace BookCrudOperationASP_Net.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        // Constructor
        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        // GET: /Books/Index
        [HttpGet]
        public IActionResult Index()
        {
            var books = _booksRepository.GetAllBooks();
            return View(books); // Return the view with the list of all books
        }

        // GET: /Books/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Return the Create view
        }

        // POST: /Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (_booksRepository.AddBook(book))
            {
                return RedirectToAction(nameof(Index)); // Redirects to the Index action if the book is successfully created
            }

            ModelState.AddModelError("", "Book creation failed"); // Adds an error to the ModelState if creation fails
            return View(book); // Returns the Create view with the error message
        }

        [HttpGet]
        public IActionResult Error()
        {
            return View(); // This could return a view named Error.cshtml, typically used for displaying error information.
        }


        // GET: /Books/Update/{id}
        [HttpGet]
        public IActionResult Update(int id)
        {
            var book = _booksRepository.GetBookById(id);
            return book != null ? View(book) : NotFound(); // Returns the Update view if the book is found, otherwise returns a 404 Not Found
        }

        // POST: /Books/Update/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Book objBook)
        {
            if (_booksRepository.UpdateBook(objBook))
            {
                return RedirectToAction(nameof(Index)); // Redirects to the Index action if the book is successfully updated
            }

            ModelState.AddModelError("", "Book update failed"); // Adds an error to the ModelState if update fails
            return View(objBook); // Returns the Update view with the error message
        }

        // POST: /Books/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (_booksRepository.DeleteBook(id))
            {
                return RedirectToAction(nameof(Index)); // Redirects to the Index action if the book is successfully deleted
            }

            return NotFound(); // Returns a 404 Not Found if deletion fails
        }
    }
}
