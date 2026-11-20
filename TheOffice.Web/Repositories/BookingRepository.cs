using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class BookingRepository(ApplicationDbContext context) : IBookingRepository
    {
        private IQueryable<Booking> GetIncludes()
        {
            return context.Bookings
            .Include(b => b.ConferenceRoom)
            .Include(b => b.Employee)
            .Include(b => b.Customer);
        }
        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await GetIncludes()
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByEmployeeIdAsync(int employeeId)
        {
            return await GetIncludes()
                .Where(b => b.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Booking>> GetByCustomerIdAsync(int customerId)
        {
            return await GetIncludes()
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await GetIncludes()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Booking> AddAsync(Booking booking)
        {
            var entityEntry = await context.Bookings.AddAsync(booking);
            await context.SaveChangesAsync();
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(Booking booking)
        {
            context.Bookings.Update(booking);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var booking = await context.Bookings.FirstOrDefaultAsync(c => c.Id == id);
            if (booking is null) return;

            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();
        }

        public async Task<bool> HasOverlapAsync(int conferenceRoomId, DateTime start, DateTime end)
        {
            return await context.Bookings.AnyAsync(b =>
                b.ConferenceRoomId == conferenceRoomId &&
                b.StartTime < end &&
                b.EndTime > start);
        }
    }
}
