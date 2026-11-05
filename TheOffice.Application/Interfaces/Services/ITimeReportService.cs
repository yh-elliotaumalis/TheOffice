using System;
using System.Collections.Generic;
using System.Text;
using TheOffice.Domain.Entities;

namespace TheOffice.Application.Interfaces.Services
{
    public interface ITimeReportService
    {
        Task<IEnumerable<TimeReport>> GetAllAsync();
        Task<TimeReport?> GetByIdAsync(int id);
        Task<TimeReport> AddAsync(TimeReport timeReport);
        Task UpdateAsync(TimeReport timeReport);
        Task DeleteAsync(int id);
    }
}
