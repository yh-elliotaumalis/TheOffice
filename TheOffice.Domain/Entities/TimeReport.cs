using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class TimeReport
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public decimal Hours { get; set; }
        public DateOnly Date { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
    }
}
