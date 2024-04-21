using Day4First.Data;
using Day4First.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day4First.Controllers
{
    public class UserController:Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Home()
        {
            List<Book> books = _dbContext.Books.ToList();
            int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            User user = _dbContext.Users.FirstOrDefault(u => u.Id == UserId);
            var ViewModel = new ViewModel();
            ViewModel.Users = user;
            ViewModel.Books = books;
            return View(ViewModel);
        }
        public IActionResult AddFavorites(Book book)
        {
            int BookId = Convert.ToInt32(book.Id);
            int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            User user = _dbContext.Users.FirstOrDefault(u => u.Id == UserId);
            if (user.BookIds.Contains(BookId))
            {
                user.BookIds.Remove(BookId);
            }
            else
            {
                user.BookIds.Add(BookId);
            }
            _dbContext.SaveChanges();
            return RedirectToAction("Home", "User");
        }
        public IActionResult BookCollection()
        {

            List<Book> books = _dbContext.Books.ToList();
            int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            User user = _dbContext.Users.FirstOrDefault(u => u.Id == UserId);
            var ViewModel = new ViewModel();
            ViewModel.Users = user;
            ViewModel.Books = books;
            return View(ViewModel);
        }
    }
}
