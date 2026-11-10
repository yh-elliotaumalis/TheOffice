using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; } = "";

        [Required(ErrorMessage = "Social Security Number is required.")]
        public string SocialNumber { get; set; } = "";

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Phone Number is required.")]
        public string Number { get; set; } = "";

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; } = "";
        public DateTime EmploymentDate { get; set; }
        public decimal Salary { get; set; }
        public string BankAccount { get; set; } = "";
        public List<TimeReport> TimeReports { get; set; } = [];
        public List<Booking> Bookings { get; set; } = [];
        public List<EmployeeAbsence> EmployeeAbsences { get; set; } = [];
        public List<WorkSchedule> WorkSchedules { get; set; } = [];
    }
}
