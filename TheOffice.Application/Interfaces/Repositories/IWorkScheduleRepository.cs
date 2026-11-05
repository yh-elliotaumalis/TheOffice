using System;
using System.Collections.Generic;
using System.Text;
using TheOffice.Domain.Entities;

namespace TheOffice.Application.Interfaces.Repositories;

public interface IWorkScheduleRepository
{
    Task<IEnumerable<WorkSchedule>> GetAllAsync();
    Task<WorkSchedule?> GetByIdAsync(int id);
    Task<WorkSchedule> AddAsync(WorkSchedule workSchedule);
    Task UpdateAsync(WorkSchedule workSchedule);
    Task DeleteAsync(int id);
}
