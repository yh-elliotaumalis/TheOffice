using Microsoft.CodeAnalysis.Host;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class WorkScheduleService(IWorkScheduleRepository workScheduleRepository) : IWorkScheduleService
    {
        public Task<WorkSchedule> AddAsync(WorkSchedule workSchedule)
        {
            return workScheduleRepository.AddAsync(workSchedule);
        }

        public Task DeleteAsync(int id)
        {
            return workScheduleRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<WorkSchedule>> GetAllAsync()
        {
            return workScheduleRepository.GetAllAsync();
        }

        public Task<WorkSchedule?> GetByIdAsync(int id)
        {
            return workScheduleRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(WorkSchedule workSchedule)
        {
            return workScheduleRepository.UpdateAsync(workSchedule);
        }
    }
}
