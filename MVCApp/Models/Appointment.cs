using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MVCApp.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        //foreign key to link to patient table
        [ForeignKey("Patient")] //need to tell ef core that this is the foreign key
        //panics and creates a whole new column 'PatientPPS' otherwise
        public string PPS { get; set; }
        public Patient Patient {  get; set; }
        //foreign key to link to doctor table
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime AppDate { get; set; }
    }
}
