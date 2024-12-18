using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        //foreign key to link to patient table
        public int PatientId { get; set; }
        public Patient Patient {  get; set; }
        //foreign key to link to doctor table
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime AppDate { get; set; }
    }
}
