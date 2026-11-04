using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class EmployeeAbsence
    {
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = "";
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
