using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class ConferenceRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public List<Booking> Bookings { get; set; } = [];

    }
}