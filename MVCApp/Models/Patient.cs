﻿using System.ComponentModel.DataAnnotations;
namespace MVCApp.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB {  get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
