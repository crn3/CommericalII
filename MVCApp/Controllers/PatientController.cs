using Microsoft.AspNetCore.Mvc;
using MVCApp.DBOperations;
using MVCApp.Models;

namespace MVCApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly DoctorsOfficeDBContext _DbContext;
        //Constructor to get a copy of the DBContext object
        public PatientController(DoctorsOfficeDBContext CopyofdbContext)
        {
            _DbContext = CopyofdbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Patient> patientList = _DbContext.Patients;
            return View(patientList);
        }
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Patient patient)
		{
			if (ModelState.IsValid)
			{
				_DbContext.Patients.Add(patient);
				_DbContext.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(patient);
		}
		public IActionResult Edit(int id) {
			if (id == 0)
			{
				return NotFound();
			}
			var patientFound = _DbContext.Patients.Find(id);
			if (patientFound == null)
				return NotFound();
			return View(patientFound);
        }
        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Patients.Update(patient);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var patientFound = _DbContext.Patients.Find(id);
            if (patientFound == null)
                return NotFound();
            return View(patientFound);
        }
        [HttpPost]
        public IActionResult Delete(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _DbContext.Patients.Remove(patient);
                _DbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
    }
}
