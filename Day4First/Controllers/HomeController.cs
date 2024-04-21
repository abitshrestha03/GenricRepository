using AspNetCoreHero.ToastNotification.Abstractions;
using Day4First.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Day4First.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpContextAccessor _sessionContxt;
        private readonly INotyfService _notyf;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, INotyfService notyf)
        {
            _logger = logger;
            _sessionContxt = httpContextAccessor;
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") == "true")
            {
                var Username = HttpContext.Session.GetString("AuthorName");
                var Id = HttpContext.Session.GetInt32("AuthorId");
                var Email = HttpContext.Session.GetString("AuthorEmail");
                return View(new { Username, Id, Email });
            }
            else
            {
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
