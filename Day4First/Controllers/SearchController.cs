using Day4First.Data;
using Day4First.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day4First.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public SearchController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Search(string Author, string Genre, string Language,string Year)
        {
            IQueryable<Book> books = _dbContext.Books.Include(a => a.Author).AsQueryable();
            var authors = books.Select(b => b.Author.Username).Distinct().ToList();
            var genres = books.Select(b => b.Genre).Distinct().ToList();
            var language = books.Select(b => b.Language).Distinct().ToList();
            var publishedYear = books.Select(b => b.PublishedDate.Year).Distinct().ToList();
            ViewBag.Authors = authors;
            ViewBag.Genres = genres;
            ViewBag.Language = language;
            ViewBag.Year = publishedYear;
            string searchQuery = Request.Query["Search"];
            if (!string.IsNullOrEmpty(searchQuery))
            {
                books=books.Where(b=>b.Title.Contains(searchQuery));
            }
            if (!string.IsNullOrEmpty(Author))
            {
                books = books.Where(b => b.Author.Username.Contains(Author));
            }
            if (!string.IsNullOrEmpty(Genre))
            {
                books = books.Where(b => b.Genre.Contains(Genre));
            }
            if (!string.IsNullOrEmpty(Language))
            {
                books = books.Where(b => b.Language.Contains(Language));
            }   
            if (!string.IsNullOrEmpty(Year))
            {
                if(int.TryParse(Year,out int year))
                books = books.Where(b => (int)b.PublishedDate.Year == year);
            }
            List<Book> model = books.ToList();
            return View(model);
        }
    }
}
