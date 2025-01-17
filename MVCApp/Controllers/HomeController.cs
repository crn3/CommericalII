using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApp.DBOperations;
using MVCApp.Models;
using System.Diagnostics;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DoctorsOfficeDBContext _DbContext;

        public HomeController(ILogger<HomeController> logger, DoctorsOfficeDBContext dbContext)
        {
            _logger = logger;
            _DbContext = dbContext;
        }

        public IActionResult Index()
        {
            DateTime today = DateTime.Today;

            var todaysAppointments = _DbContext.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Where(a => a.AppDate.Date == today)
                .OrderBy(a => a.AppDate)
                .ToList();


            return View(todaysAppointments);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
