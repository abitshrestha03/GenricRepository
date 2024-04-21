using Day4First.Data;
using Day4First.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Day4First.Controllers
{
    public class CommentController:Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CommentController(ApplicationDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            var LoggedIn = HttpContext.Session.GetString("IsLoggedIn");
            if (LoggedIn != "true")
            {
                return RedirectToAction("Login", "Auth");
            }
            int UserId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            comment.UserId = UserId;
            comment.PostedAt = DateTime.Now;
            _dbContext.Comments.Add(comment);
            _dbContext.SaveChanges();
            return RedirectToAction("Home", "User");
        }
        public IActionResult Comment()
        {
            List<Comment> comments = _dbContext.Comments.Include(c=>c.User).ToList();
            return View(comments);
        }
    }
}
