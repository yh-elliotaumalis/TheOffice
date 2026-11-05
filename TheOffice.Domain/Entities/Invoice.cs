using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = "";
        public DateOnly DeadLine { get; set; }
        public DateOnly DateSent { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public decimal HourlyRate { get; set; }
        public Project? Project { get; set; }
        public int ProjectId { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}
