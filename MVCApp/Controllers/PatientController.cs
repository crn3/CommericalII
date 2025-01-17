using Microsoft.AspNetCore.Mvc;
using MVCApp.DBOperations;
using MVCApp.Models;

namespace MVCApp.Controllers
{
	public class PatientController : Controller
	{
		private readonly DoctorsOfficeDBContext _DbContext;

		// Constructor to get a copy of the DBContext object
		public PatientController(DoctorsOfficeDBContext CopyofdbContext)
		{
			_DbContext = CopyofdbContext;
		}

		public IActionResult Index(string searchString)
		{
			IEnumerable<Patient> patientList = _DbContext.Patients;

			if (!String.IsNullOrEmpty(searchString))
			{
				patientList = patientList.Where(p =>
				p.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
				p.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
			}

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

		public IActionResult Edit(string id) //don't change this to pps
			//different id variable than the original id given to patients
		{
			if (string.IsNullOrEmpty(id))
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

		public IActionResult Delete(string id)
		{
			if (string.IsNullOrEmpty(id))
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
