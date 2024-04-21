using Day4First.Data;
using Day4First.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Day4First.Controllers
{
    public class BookController:Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(ApplicationDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public IActionResult Book(Book book, IFormFile? Image)
        {
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

                book.Image = @"/Image/Upload/" + fileName;
            }
            _dbContext.Add(book);
            _dbContext.SaveChanges();
            return RedirectToAction("Home","Author");
        }
            public IActionResult LikeAddOrRemove(Book book)
            {
                var LoggedIn = HttpContext.Session.GetString("IsLoggedIn");
                int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
                if (LoggedIn != "true")
                {
                    return RedirectToAction("Login", "Auth");
                }
                Book bookFound =_dbContext.Books.Where(b=>b.Id==book.Id).FirstOrDefault();
            if (bookFound.LikedBy.Contains(UserId))
            {
                bookFound.LikedBy.Remove(UserId);
                bookFound.Likes--;
            }
            else
            {
                bookFound.LikedBy.Add(UserId);
                bookFound.Likes++;
            }

            _dbContext.SaveChanges();
            return RedirectToAction("Home", "User");
        }
    }
}
