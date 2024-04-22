using Day4First.Data;
using Day4First.Models;
using Day4First.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Day4First.Controllers
{
    public class AuthController:Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuthor _authorRepository;
        private readonly IUserRepository _userRepository;
        private readonly SessionService _sessionService;
        public AuthController(IAuthor authorRepository, SessionService sessionService,ApplicationDbContext dbContext,IUserRepository userRepository)
        {
            _authorRepository = authorRepository;
            _sessionService = sessionService;
            _dbContext = dbContext;
            _userRepository = userRepository;
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(IFormCollection formCollection) {
            string username = formCollection["Username"];
            string email = formCollection["Email"];
            string password = formCollection["Password"];
            bool isAuthor = bool.Parse(formCollection["IsAuthor"]);
            if (isAuthor)
            {
                Author author = new Author
                {
                    Username = username,
                    Email = email,
                    Password = password
                };
                Author authorEmailFound = await _authorRepository.InsertAuthorAsync(author);
                if (authorEmailFound != null)
                {
                    ViewBag.errorMessage = "Email " + "'" + authorEmailFound.Email + "'" + " already taken!";
                    return View();
                }
            }
            else
            {
                User user = new User
                {
                    Username = username,
                    Email = email,
                    Password = password
                };
                User userEmailFound = await _userRepository.InsertUserAsync(user);
                if (userEmailFound != null)
                {
                    ViewBag.errorMessage = "Email " + "'" + userEmailFound.Email + "'" + " already taken!";
                    return View();
                }
            }
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(IFormCollection formCollection)
        {
            string email = formCollection["Email"];
            string password = formCollection["Password"];
            bool isAuthor = bool.Parse(formCollection["IsAuthor"]);
            if (isAuthor)
            {
                Author author = new Author
                {
                    Email = email,
                    Password = password
                };
                AuthorCheckResult result = await _authorRepository.CheckAuthor(author);
                if (!result.IsSuccessful)
                {
                    ViewBag.errorMessage = result.Message;
                    return View();
                }
                else
                {
                    ViewBag.successMessage = result.Message;
                    _sessionService.SetAuthorSessionValue(result);
                    return RedirectToAction("Home","Author");
                }
            }
            else
            {
                User user = new User
                {
                    Email = email,
                    Password = password
                };
                UserCheckResult result = await _userRepository.CheckUser(user);
                if (!result.IsSuccessful)
                {
                    ViewBag.errorMessage = result.Message;
                    return View();
                }
                else
                {
                    ViewBag.successMessage = result.Message;
                    _sessionService.SetUserSessionValue(result);
                    return RedirectToAction("Home","User");
                }
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Home", "User");
        }
    }
}
