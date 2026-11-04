using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string Description { get; set; } = "";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public ConferenceRoom ConferenceRoom { get; set; }
        public int ConferenceRoomId { get; set; }
    }
}
