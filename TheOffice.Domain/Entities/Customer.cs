using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Address { get; set; } = "";
        public string Notes { get; set; } = "";
        public List<Project> Projects { get; set; } = [];
        public List<Booking> Bookings { get; set; } = [];
        public List<Invoice> Invoices { get; set; } = [];
    }
}