using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class WorkScheduleRepository(ApplicationDbContext context) : IWorkScheduleRepository
    {
        public async Task<IEnumerable<WorkSchedule>> GetAllAsync()
        {
            return await context.WorkSchedules.ToListAsync();
        }

        public async Task<WorkSchedule?> GetByIdAsync(int id)
        {
            return await context.WorkSchedules
                .FirstOrDefaultAsync(ws => ws.Id == id);
        }
        public async Task<WorkSchedule> AddAsync(WorkSchedule workSchedule)
        {
            var entityEntry = await context.WorkSchedules.AddAsync(workSchedule);
            await context.SaveChangesAsync();
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(WorkSchedule workSchedule)
        {
            context.WorkSchedules.Update(workSchedule);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var workSchedule = await context.WorkSchedules.FirstOrDefaultAsync(ws => ws.Id == id);
            if (workSchedule is null) return;

            context.WorkSchedules.Remove(workSchedule);
            await context.SaveChangesAsync();
        }
    }
}
