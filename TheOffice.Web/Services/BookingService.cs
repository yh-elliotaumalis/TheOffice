using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {

        public Task<Booking> AddAsync(Booking booking)
        {
            return bookingRepository.AddAsync(booking);
        }

        public Task<bool> HasOverlapAsync(int conferenceRoomId, DateTime start, DateTime end)
        {
            return bookingRepository.HasOverlapAsync(conferenceRoomId, start, end);
        }

        public Task DeleteAsync(int id)
        {
            return bookingRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Booking>> GetAllAsync()
        {
            return bookingRepository.GetAllAsync();
        }

        public Task<Booking?> GetByIdAsync(int id)
        {
            return bookingRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Booking booking)
        {
            return bookingRepository.UpdateAsync(booking);
        }
    }
}
