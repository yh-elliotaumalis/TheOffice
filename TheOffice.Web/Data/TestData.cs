using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Data
{
    public class TestData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            if (!await context.Employees.AnyAsync())
            {
                var employees = new List<Employee>
                {
                    new Employee { FullName = "Anna Lindqvist", SocialNumber = "19880412-4321", Title = "Systems Developer", Email = "anna.lindqvist@theoffice.com", Number = "+46 70-123 45 67", Address = "12 Main Street, Gothenburg", EmploymentDate = new DateTime(2021, 3, 15), Salary = 48500m, BankAccount = "8105-9, 123 456 789-0" },
                    new Employee { FullName = "Erik Bergström", SocialNumber = "19821103-8765", Title = "Project Manager", Email = "erik.bergstrom@theoffice.com", Number = "+46 73-987 65 43", Address = "45 King Street, Gothenburg", EmploymentDate = new DateTime(2019, 9, 1), Salary = 54000m, BankAccount = "5001-12 345 67" },
                    new Employee { FullName = "Sara Nyström", SocialNumber = "19950622-1122", Title = "UX/UI Designer", Email = "sara.nystrom@theoffice.com", Number = "+46 76-555 44 33", Address = "8 Linné Street, Gothenburg", EmploymentDate = new DateTime(2023, 1, 10), Salary = 42000m, BankAccount = "6123-456 789 012" },
                    new Employee { FullName = "Johan Ek", SocialNumber = "19790218-9988", Title = "Finance Manager", Email = "johan.ek@theoffice.com", Number = "+46 70-222 33 44", Address = "21 The Avenue, Gothenburg", EmploymentDate = new DateTime(2018, 5, 20), Salary = 51500m, BankAccount = "9020-11 223 34" },
                    new Employee { FullName = "Maja Holm", SocialNumber = "19990930-5544", Title = "HR Specialist", Email = "maja.holm@theoffice.com", Number = "+46 72-888 77 66", Address = "33 Vasa Street, Gothenburg", EmploymentDate = new DateTime(2024, 8, 12), Salary = 39000m, BankAccount = "3300-9909305544" }
                };

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }

            if (!await context.Customers.AnyAsync())
            {
                var customers = new List<Customer>
                {
                    new Customer { Name = "Nordic Tech Corp", Email = "contact@nordictech.com", PhoneNumber = "+46 31-12 34 56", Address = "5 Lindholmspiren, Gothenburg", Notes = "Priority IT client with an ongoing support agreement." },
                    new Customer { Name = "West Coast Construction & Real Estate", Email = "info@westcoastbuild.com", PhoneNumber = "+46 31-98 76 54", Address = "14 Export Street, Hisings Backa", Notes = "Always billed upon completion of each project milestone." },
                    new Customer { Name = "Sunflower Café Ltd", Email = "hello@sunflowercafe.com", PhoneNumber = "+46 70-444 55 66", Address = "18 Haga New Street, Gothenburg", Notes = "Needs a new website and table booking system by spring." },
                    new Customer { Name = "Gothenburg Logistics Partner", Email = "finance@gbglogistics.com", PhoneNumber = "+46 31-55 66 77", Address = "22 Arendal Road, Gothenburg", Notes = "Point of contact is Anders; prefers email communication." },
                    new Customer { Name = "Studio Green Design", Email = "hello@studiogreen.com", PhoneNumber = "+46 73-111 22 33", Address = "10 Magazine Street, Gothenburg", Notes = "Partner agency for branding and graphic design." }
                };

                await context.Customers.AddRangeAsync(customers);
                await context.SaveChangesAsync();
            }

            if (!await context.Projects.AnyAsync())
            {
                var projects = new List<Project>
                {
                    new Project { Name = "Cloud Migration & Security Upgrade", IsCompleted = false, CustomerId = 1 },
                    new Project { Name = "New Project Management Portal", IsCompleted = true, CustomerId = 2 },
                    new Project { Name = "Website & Table Booking System", IsCompleted = false, CustomerId = 3 },
                    new Project { Name = "Automated Inventory Sync", IsCompleted = false, CustomerId = 4 },
                    new Project { Name = "Brand Identity & Design System", IsCompleted = true, CustomerId = 5 }
                };

                await context.Projects.AddRangeAsync(projects);
                await context.SaveChangesAsync();
            }

            if (!await context.WorkSchedules.AnyAsync())
            {
                var workschedules = new List<WorkSchedule>
                {
                    new WorkSchedule { StartDate = new DateTime(2026, 11, 23, 8, 0, 0), EndDate = new DateTime(2026, 11, 23, 17, 0, 0), EmployeeId = 1 },
                    new WorkSchedule { StartDate = new DateTime(2026, 11, 23, 9, 0, 0), EndDate = new DateTime(2026, 11, 23, 18, 0, 0), EmployeeId = 2 },
                    new WorkSchedule { StartDate = new DateTime(2026, 11, 24, 8, 30, 0), EndDate = new DateTime(2026, 11, 24, 17, 30, 0), EmployeeId = 3 },
                    new WorkSchedule { StartDate = new DateTime(2026, 11, 24, 7, 30, 0), EndDate = new DateTime(2026, 11, 24, 16, 30, 0), EmployeeId = 4 },
                    new WorkSchedule { StartDate = new DateTime(2026, 11, 25, 8, 0, 0), EndDate = new DateTime(2026, 11, 25, 16, 0, 0), EmployeeId = 5 }
                };

                await context.WorkSchedules.AddRangeAsync(workschedules);
                await context.SaveChangesAsync();
            }

            if (!await context.Invoices.AnyAsync())
            {
                var invoices = new List<Invoice>
                {
                    new Invoice { InvoiceNumber = "INV-2026-001", DeadLine = new DateOnly(2026, 12, 20), DateSent = new DateOnly(2026, 11, 20), TotalAmount = 45000m, IsPaid = false, HourlyRate = 900m, ProjectId = 1, CustomerId = 1 },
                    new Invoice { InvoiceNumber = "INV-2026-002", DeadLine = new DateOnly(2026, 11, 15), DateSent = new DateOnly(2026, 10, 15), TotalAmount = 85000m, IsPaid = true, HourlyRate = 850m, ProjectId = 2, CustomerId = 2 },
                    new Invoice { InvoiceNumber = "INV-2026-003", DeadLine = new DateOnly(2026, 12, 10), DateSent = new DateOnly(2026, 11, 10), TotalAmount = 22500m, IsPaid = false, HourlyRate = 750m, ProjectId = 3, CustomerId = 3 },
                    new Invoice { InvoiceNumber = "INV-2026-004", DeadLine = new DateOnly(2026, 12, 23), DateSent = new DateOnly(2026, 11, 23), TotalAmount = 66500m, IsPaid = false, HourlyRate = 950m, ProjectId = 4, CustomerId = 4 },
                    new Invoice { InvoiceNumber = "INV-2026-005", DeadLine = new DateOnly(2026, 10, 30), DateSent = new DateOnly(2026, 9, 30), TotalAmount = 32000m, IsPaid = true, HourlyRate = 800m, ProjectId = 5, CustomerId = 5 }
                };

                await context.Invoices.AddRangeAsync(invoices);
                await context.SaveChangesAsync();
            }

            if (!await context.TimeReports.AnyAsync())
            {
                var timeReports = new List<TimeReport>
                {
                    new TimeReport { Description = "Configured Azure cloud environment and firewall rules", Hours = 8.0m, Date = new DateOnly(2026, 11, 23), ProjectId = 1, EmployeeId = 1 },
                    new TimeReport { Description = "Sprint planning and client stakeholder alignment", Hours = 6.5m, Date = new DateOnly(2026, 11, 23), ProjectId = 2, EmployeeId = 2 },
                    new TimeReport { Description = "Designed wireframes for table reservation flow", Hours = 7.5m, Date = new DateOnly(2026, 11, 24), ProjectId = 3, EmployeeId = 3 },
                    new TimeReport { Description = "Reviewed warehouse API billing and cost estimates", Hours = 4.0m, Date = new DateOnly(2026, 11, 24), ProjectId = 4, EmployeeId = 4 },
                    new TimeReport { Description = "Prepared onboarding guidelines for design team", Hours = 5.0m, Date = new DateOnly(2026, 11, 25), ProjectId = 5, EmployeeId = 5 }
                };

                await context.TimeReports.AddRangeAsync(timeReports);
                await context.SaveChangesAsync();
            }

            if (!await context.ConferenceRooms.AnyAsync())
            {
                var conferenceRooms = new List<ConferenceRoom>
                {
                    new ConferenceRoom { Name = "The Boardroom" },
                    new ConferenceRoom { Name = "Innovation Lab" },
                    new ConferenceRoom { Name = "Harbor View" },
                    new ConferenceRoom { Name = "Focus Pod A" },
                    new ConferenceRoom { Name = "Skyline Lounge" }
                };

                await context.ConferenceRooms.AddRangeAsync(conferenceRooms);
                await context.SaveChangesAsync();
            }

            if (!await context.EmployeeAbsences.AnyAsync())
            {
                var employeeAbsences = new List<EmployeeAbsence>
                {
                    new EmployeeAbsence { Type = "Sick Leave", StartDate = new DateTime(2026, 11, 2, 8, 0, 0), EndDate = new DateTime(2026, 11, 4, 17, 0, 0), Description = "Seasonal flu recovery", EmployeeId = 1 },
                    new EmployeeAbsence { Type = "Vacation", StartDate = new DateTime(2026, 12, 21, 8, 0, 0), EndDate = new DateTime(2026, 12, 31, 17, 0, 0), Description = "Christmas and New Year holiday", EmployeeId = 2 },
                    new EmployeeAbsence { Type = "Parental Leave", StartDate = new DateTime(2026, 11, 16, 8, 0, 0), EndDate = new DateTime(2026, 11, 20, 17, 0, 0), Description = "Scheduled parental leave with newborn", EmployeeId = 3 },
                    new EmployeeAbsence { Type = "Care Of Sick Child", StartDate = new DateTime(2026, 11, 26, 13, 0, 0), EndDate = new DateTime(2026, 11, 26, 15, 0, 0), Description = "Pediatrician visit with sick child", EmployeeId = 4 },
                    new EmployeeAbsence { Type = "Vacation", StartDate = new DateTime(2026, 12, 4, 8, 0, 0), EndDate = new DateTime(2026, 12, 4, 17, 0, 0), Description = "Day off for moving to a new apartment", EmployeeId = 5 }
                };

                await context.EmployeeAbsences.AddRangeAsync(employeeAbsences);
                await context.SaveChangesAsync();
            }

            if (!await context.Bookings.AnyAsync())
            {
                var bookings = new List<Booking>
                {
                    new Booking { Description = "Cloud migration architecture review", StartTime = new DateTime(2026, 11, 23, 10, 0, 0), EndTime = new DateTime(2026, 11, 23, 11, 30, 0), EmployeeId = 1, CustomerId = 1, ConferenceRoomId = 1 },
                    new Booking { Description = "Project portal kickoff and sprint demo", StartTime = new DateTime(2026, 11, 23, 13, 0, 0), EndTime = new DateTime(2026, 11, 23, 14, 30, 0), EmployeeId = 2, CustomerId = 2, ConferenceRoomId = 2 },
                    new Booking { Description = "UI/UX wireframe presentation for booking flow", StartTime = new DateTime(2026, 11, 24, 9, 30, 0), EndTime = new DateTime(2026, 11, 24, 11, 0, 0), EmployeeId = 3, CustomerId = 3, ConferenceRoomId = 3 },
                    new Booking { Description = "Q4 budget and inventory sync contract review", StartTime = new DateTime(2026, 11, 24, 14, 0, 0), EndTime = new DateTime(2026, 11, 24, 15, 0, 0), EmployeeId = 4, CustomerId = 4, ConferenceRoomId = 4 },
                    new Booking { Description = "Brand identity workshop and creative alignment", StartTime = new DateTime(2026, 11, 25, 10, 0, 0), EndTime = new DateTime(2026, 11, 25, 12, 0, 0), EmployeeId = 5, CustomerId = 5, ConferenceRoomId = 5 }
                };

                await context.Bookings.AddRangeAsync(bookings);
                await context.SaveChangesAsync();
            }
        }
    }
}