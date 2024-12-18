using Microsoft.EntityFrameworkCore;
using MVCApp.Models;

namespace MVCApp.DBOperations
{
    public class DoctorsOfficeDBContext :DbContext
    {
        public DoctorsOfficeDBContext(DbContextOptions<DoctorsOfficeDBContext>
            options) :base(options) { }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

    }
}
