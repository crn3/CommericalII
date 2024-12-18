using Microsoft.AspNetCore.Mvc;
using MVCApp.DBOperations;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorsOfficeDBContext _DbContext;
        //Constructor to get a copy of the DBContext object
        public DoctorController(DoctorsOfficeDBContext CopyofdbContext)
        {
            _DbContext = CopyofdbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Doctor> doctorList = _DbContext.Doctors;
            return View(doctorList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Doctors.Add(doctor);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }
        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var doctorFound = _DbContext.Doctors.Find(id);
            if (doctorFound == null)
                return NotFound();
            return View(doctorFound);
        }
        [HttpPost]
        public IActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Doctors.Update(doctor);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var doctorFound = _DbContext.Doctors.Find(id);
            if (doctorFound == null)
                return NotFound();
            return View(doctorFound);
        }
        [HttpPost]
        public IActionResult Delete(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Doctors.Remove(doctor);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }
    }
}
