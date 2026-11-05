using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class TimeReportService(ITimeReportRepository timeReportRepository) : ITimeReportService
    {
        public Task<TimeReport> AddAsync(TimeReport timeReport)
        {
            return timeReportRepository.AddAsync(timeReport);
        }

        public Task DeleteAsync(int id)
        {
            return timeReportRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<TimeReport>> GetAllAsync()
        {
            return timeReportRepository.GetAllAsync();
        }

        public Task<TimeReport?> GetByIdAsync(int id)
        {
            return timeReportRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(TimeReport timeReport)
        {
            return timeReportRepository.UpdateAsync(timeReport);
        }
    }
}
