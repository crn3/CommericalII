using Microsoft.AspNetCore.Mvc;
using MVCApp.DBOperations;
using MVCApp.Models;
using System.Runtime.CompilerServices;

namespace MVCApp.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DoctorsOfficeDBContext _DbContext;
        //Constructor to get a copy of the DBContext object
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DoctorController(DoctorsOfficeDBContext CopyofdbContext, IWebHostEnvironment webHostEnvironment)
        {
            _DbContext = CopyofdbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index(string searchString)
        {
            IEnumerable<Doctor> doctorList = _DbContext.Doctors;

            if (!String.IsNullOrEmpty(searchString))
            {
                doctorList = doctorList.Where(d =>
                d.FirstName.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                d.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }
            
            return View(doctorList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"Images/Doctor/");
                    var extension = Path.GetExtension(file.FileName);
                    
                    using (var filestreams = new FileStream(Path.Combine(uploads, filename +extension), FileMode.Create))
                    {
                        file.CopyTo(filestreams);
                    }
                    doctor.Image = @"Images/Doctor/" + filename + extension;
                }

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
        public IActionResult Edit(Doctor doctor, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootFolder = _webHostEnvironment.WebRootPath;

                if(file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(wwwRootFolder, @"Images/Doctor");
                    var extension = Path.GetExtension(file.FileName);

                    if (doctor.Image != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootFolder, doctor.Image.Trim('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(upload, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    // stops image link having mix of forward and back slashes
                    doctor.Image = Path.Combine("Images/Doctor", filename + extension).Replace("\\", "/");

                }

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
