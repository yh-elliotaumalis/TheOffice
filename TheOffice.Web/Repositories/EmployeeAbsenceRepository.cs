using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class EmployeeAbsenceRepository(ApplicationDbContext context) : IEmployeeAbsenceRepository
    {
        public async Task<IEnumerable<EmployeeAbsence>> GetAllAsync()
        {
            return await context.EmployeeAbsences.ToListAsync();
        }

        public async Task<EmployeeAbsence?> GetByIdAsync(int id)
        {
            return await context.EmployeeAbsences
                .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<EmployeeAbsence> AddAsync(EmployeeAbsence employeeAbsence)
        {
            var entityEntry = await context.EmployeeAbsences.AddAsync(employeeAbsence);
            await context.SaveChangesAsync();
            return entityEntry.Entity;
        }
        public async Task UpdateAsync(EmployeeAbsence employeeAbsence)
        {
            context.EmployeeAbsences.Update(employeeAbsence);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var employeeAbsence = await context.EmployeeAbsences.FirstOrDefaultAsync(e => e.Id == id);
            if (employeeAbsence is null) return;

            context.EmployeeAbsences.Remove(employeeAbsence);
            await context.SaveChangesAsync();
        }
    }
}
