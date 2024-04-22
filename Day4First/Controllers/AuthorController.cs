using Day4First.Data;
using Day4First.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Day4First.Controllers
{
    public class AuthorController:Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthorController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Home()
        {
            var IsLoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            var IsAuthor = HttpContext.Session.GetString("IsAuthor");
            if (IsLoggedIn!="true")
            {
                return RedirectToAction("Login", "Auth");
            }
            if (IsAuthor != "true") {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }
        public IActionResult MyBooks()
        {
            var id = HttpContext.Session.GetInt32("AuthorId");
            IQueryable<Book> books=_dbContext.Books.Where(b=>b.AuthorId==id).Include(b=>b.Author).AsQueryable();
            return View(books.ToList());
        }
        public IActionResult Edit(Book book)
        {
            ViewBag.Genres = _dbContext.Books.Select(b => b.Genre).Distinct().ToList();
            Book ToEditBook = _dbContext.Books.FirstOrDefault(b => b.Id == book.Id);
            return View(ToEditBook);
        }
        [HttpPost]
        public IActionResult EditBook(Book editBook, IFormFile? Image)
        {
            Book bookToEdit = _dbContext.Books.FirstOrDefault(b => b.Id == editBook.Id);
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                string productPath = Path.Combine(wwwRootPath, "Image", "Upload");
                string filePath = Path.Combine(productPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Image.CopyTo(fileStream);
                }

                bookToEdit.Image = @"/Image/Upload/" + fileName;
            }
            bookToEdit.Title = editBook.Title;
            bookToEdit.Genre = editBook.Genre;
            bookToEdit.Language = editBook.Language;
            _dbContext.SaveChanges();
            return RedirectToAction("MyBooks", "Author");
        }
        public IActionResult Delete(Book book)
        {
            Book bookToEdit = _dbContext.Books.FirstOrDefault(b => b.Id == book.Id);
            _dbContext.Books.Remove(bookToEdit);
            _dbContext.SaveChanges();
            return RedirectToAction("MyBooks", "Author");
        }
    }
}
