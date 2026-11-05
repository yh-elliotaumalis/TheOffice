using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class ConferenceRoomService(IConferenceRoomRepository conferenceRoomRepository) : IConferenceRoomService
    {
        public Task<ConferenceRoom> AddAsync(ConferenceRoom conferenceRoom)
        {
            return conferenceRoomRepository.AddAsync(conferenceRoom);
        }

        public Task DeleteAsync(int id)
        {
            return conferenceRoomRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<ConferenceRoom>> GetAllAsync()
        {
            return conferenceRoomRepository.GetAllAsync();
        }

        public Task<ConferenceRoom?> GetByIdAsync(int id)
        {
            return conferenceRoomRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(ConferenceRoom conferenceRoom)
        {
            return conferenceRoomRepository.UpdateAsync(conferenceRoom);
        }
    }
}
