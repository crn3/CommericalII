using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCApp.DBOperations;
using MVCApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCApp.Controllers
{
	public class AppointmentController : Controller
	{
		private readonly DoctorsOfficeDBContext _DbContext;

        public AppointmentController(DoctorsOfficeDBContext CopyofdbContext)
        {
            _DbContext = CopyofdbContext;
        }

		public IActionResult Index(string searchString)
		{
			IEnumerable<Appointment> appointmentList = _DbContext.Appointments
				.Include(p => p.Patient)
				.Include(a => a.Doctor)
				.OrderBy(a => a.AppDate)
				.ToList();//null reference error unless you force it to load data with ToList

			if (!string.IsNullOrEmpty(searchString))
			{
				appointmentList = appointmentList.Where(a => 
                    (a.Patient.FirstName + " " + a.Patient.LastName).Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
					(a.Doctor.FirstName + " " + a.Doctor.LastName).Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            return View(appointmentList);
		}

		[HttpGet]
		public IActionResult Create()
		{
			IEnumerable<SelectListItem> PatientList = _DbContext.Patients
				.Select(n => new SelectListItem
				{
					Text = $"{n.FirstName} {n.LastName}",
					Value = n.PPS
				})
				.ToList();

			IEnumerable<SelectListItem> DoctorList = _DbContext.Doctors.Select(
				d => new SelectListItem
				{
					Text = $"{d.FirstName} {d.LastName}",
					Value = d.Id.ToString()
				})
				.ToList();

			ViewBag.PatientList = PatientList;
			ViewBag.DoctorList = DoctorList;

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
			var apptFound = _DbContext.Appointments
				.Include(a => a.Patient)
				.Include(a => a.Doctor)
				.FirstOrDefault(a => a.Id == id);

			if (apptFound == null)
				return NotFound();

			ViewBag.PatientList = _DbContext.Patients
				.Select(p => new SelectListItem
				{
					Text = $"{p.FirstName} {p.LastName}",
					Value = p.PPS
				}).ToList();

			ViewBag.DoctorList = _DbContext.Doctors
				.Select(d => new SelectListItem
				{
					Text = $"{d.FirstName} {d.LastName}",
					Value = d.Id.ToString()
				}).ToList();

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
