using System;
using System.Collections.Generic;
using System.Text;

namespace TheOffice.Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsCompleted { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}