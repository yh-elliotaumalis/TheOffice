using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class TimeReportRepository(ApplicationDbContext context) : ITimeReportRepository
    {
        public async Task<IEnumerable<TimeReport>> GetAllAsync()
        {
            return await context.TimeReports.ToListAsync();
        }

        public async Task<TimeReport?> GetByIdAsync(int id)
        {
            return await context.TimeReports
                .FirstOrDefaultAsync(tr => tr.Id == id);
        }
        public async Task<TimeReport> AddAsync(TimeReport timeReport)
        {
            var entityEntry = await context.TimeReports.AddAsync(timeReport);
            await context.SaveChangesAsync();
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(TimeReport timeReport)
        {
            context.TimeReports.Update(timeReport);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var timeReport = await context.TimeReports.FirstOrDefaultAsync(tr => tr.Id == id);
            if (timeReport is null) return;

            context.TimeReports.Remove(timeReport);
            await context.SaveChangesAsync();
        }
    }
}
