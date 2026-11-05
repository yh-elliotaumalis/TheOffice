using System;
using System.Collections.Generic;
using System.Text;
using TheOffice.Domain.Entities;

namespace TheOffice.Application.Interfaces.Services
{
    public interface IConferenceRoomService
    {
        Task<IEnumerable<ConferenceRoom>> GetAllAsync();
        Task<ConferenceRoom?> GetByIdAsync(int id);
        Task<ConferenceRoom> AddAsync(ConferenceRoom conferenceRoom);
        Task UpdateAsync(ConferenceRoom conferenceRoom);
        Task DeleteAsync(int id);
    }
}
