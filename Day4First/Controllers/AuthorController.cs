using Microsoft.AspNetCore.Mvc;

namespace Day4First.Controllers
{
    public class AuthorController:Controller
    {
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
    }
}
