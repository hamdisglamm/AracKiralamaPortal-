using System.Diagnostics;
using AracKiralamaPortal.Models;
using Microsoft.AspNetCore.Mvc;

namespace AracKiralamaPortal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Özelleştirilmiş Anasayfa Mesajı
        public IActionResult Index()
        {
            ViewData["WelcomeMessage"] = "Hamdi'nin Garajı'na Hoşgeldiniz!";
            return View();
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
