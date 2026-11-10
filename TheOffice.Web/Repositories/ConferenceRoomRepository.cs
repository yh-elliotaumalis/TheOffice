using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class ConferenceRoomRepository(ApplicationDbContext context) : IConferenceRoomRepository
    {
        public IQueryable<ConferenceRoom> GetConferenceRoomsWithBookings()
        {
            return context.ConferenceRooms.Include(c => c.Bookings);
        }

        public async Task<IEnumerable<ConferenceRoom>> GetAllAsync()
        {
            return await GetConferenceRoomsWithBookings().ToListAsync();
        }

        public async Task<ConferenceRoom?> GetByIdAsync(int id)
        {
            return await GetConferenceRoomsWithBookings()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<ConferenceRoom> AddAsync(ConferenceRoom conferenceRoom)
        {
            var entityEntry = await context.ConferenceRooms.AddAsync(conferenceRoom);
            await context.SaveChangesAsync();
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(ConferenceRoom conferenceRoom)
        {
            context.ConferenceRooms.Update(conferenceRoom);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var conferenceRoom = await context.ConferenceRooms.FirstOrDefaultAsync(c => c.Id == id);
            if (conferenceRoom is null) return;

            context.ConferenceRooms.Remove(conferenceRoom);
            await context.SaveChangesAsync();
        }
    }
}
