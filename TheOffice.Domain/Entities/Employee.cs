using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string SocialNumber { get; set; } = "";
        public string Title { get; set; } = "";
        public string Email { get; set; } = "";
        public string Number { get; set; } = "";
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
