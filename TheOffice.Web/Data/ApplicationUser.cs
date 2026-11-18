using Microsoft.AspNetCore.Identity;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }

}
