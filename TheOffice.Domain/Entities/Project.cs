using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Project Name is required.")]
        public string Name { get; set; } = "";
        public bool IsCompleted { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public List<Invoice> Invoices { get; set; } = [];
    }
}