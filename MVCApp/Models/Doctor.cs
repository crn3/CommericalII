using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialisation { get; set; }
        public List<Appointment>Appointments { get; set; }
    }
}
