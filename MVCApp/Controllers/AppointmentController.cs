using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApp.DBOperations;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly DoctorsOfficeDBContext _DbContext;
        //Constructor to get a copy of the DBContext object
        public AppointmentController(DoctorsOfficeDBContext CopyofdbContext)
        {
            _DbContext = CopyofdbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Appointment> appointmentList = _DbContext.Appointments
                .Include(p => p.Patient)
                .Include(a => a.Doctor)
                .ToList(); //null reference error unless you force it to load data with ToList
            return View(appointmentList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Appointments.Add(appointment);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var apptFound = _DbContext.Appointments.Find(id);
            if (apptFound == null)
                return NotFound();
            return View(apptFound);
        }
        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Appointments.Update(appointment);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var apptFound = _DbContext.Appointments.Find(id);
            if (apptFound == null)
                return NotFound();
            return View(apptFound);
        }
        [HttpPost]
        public IActionResult Delete(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Appointments.Remove(appointment);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appointment);
        }
    }
}
