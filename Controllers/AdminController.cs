using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AracKiralamaPortal.Data;
using AracKiralamaPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace AracKiralamaPortal.Controllers
{
    [Authorize(Roles = "Admin")] // Yalnızca Admin rolüne sahip kullanıcılar erişebilir
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Yönetici Paneli Ana Sayfası
        public IActionResult Index()
        {
            return View();
        }

        // Araç Listesi Yönetimi
        public async Task<IActionResult> ManageCars()
        {
            var cars = await _context.Cars.ToListAsync();
            return View(cars);
        }

        // Yeni Araç Ekleme GET
        public IActionResult AddCar()
        {
            return View();
        }

        // Yeni Araç Ekleme POST
        [HttpPost]
        public async Task<IActionResult> AddCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction("ManageCars");
            }
            return View(car);
        }

        // Kullanıcı Listesi Yönetimi
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);
        }
    }
}
