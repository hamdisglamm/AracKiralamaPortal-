using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AracKiralamaPortal.Data;
using AracKiralamaPortal.Models;
using Microsoft.AspNetCore.SignalR;
using AracKiralamaPortal.Hubs;

namespace AracKiralamaPortal.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Araçları Listeleme
        public async Task<IActionResult> Index(string searchBrand)
        {
            var cars = string.IsNullOrEmpty(searchBrand)
                ? await _unitOfWork.Cars.GetAllAsync()
                : await _unitOfWork.Cars.FindAsync(c => c.IsAvailable && c.Brand.Contains(searchBrand));

            return View(cars);
        }

        // Araç Detayı ve Rezervasyon Sayfası
        public async Task<IActionResult> Details(int id)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(id);

            if (car == null)
            {
                TempData["ErrorMessage"] = "Araç bulunamadı. Lütfen geçerli bir araç seçiniz.";
                return RedirectToAction("Index");
            }

            if (!car.IsAvailable)
            {
                TempData["ErrorMessage"] = "Bu araç başka bir kullanıcımız tarafından kiralanmıştır. Lütfen başka bir araç seçiniz.";
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // Rezervasyon Yapma
        [HttpPost]
        public async Task<IActionResult> Reserve(int id, DateTime startDate, DateTime endDate, [FromServices] IHubContext<CarHub> hubContext)
        {
            var car = await _unitOfWork.Cars.GetByIdAsync(id);
            if (car == null || !car.IsAvailable || startDate >= endDate)
                return BadRequest("Geçersiz rezervasyon bilgisi!");

            var reservation = new Reservation
            {
                CarId = id,
                UserId = User.Identity.Name,
                StartDate = startDate,
                EndDate = endDate
            };

            await _unitOfWork.Reservations.AddAsync(reservation);
            car.IsAvailable = false;

            _unitOfWork.Cars.Update(car);
            await _unitOfWork.SaveAsync();

            await hubContext.Clients.All.SendAsync("CarUpdated"); // SignalR ile bildirim

            return RedirectToAction("Index");
        }

        // AJAX ile araç filtreleme
        [HttpGet]
        public async Task<JsonResult> FilterCars(string searchBrand)
        {
            var cars = string.IsNullOrEmpty(searchBrand)
                ? await _unitOfWork.Cars.FindAsync(c => c.IsAvailable)
                : await _unitOfWork.Cars.FindAsync(c => c.IsAvailable && c.Brand.Contains(searchBrand));

            return Json(cars);
        }
    }
}
