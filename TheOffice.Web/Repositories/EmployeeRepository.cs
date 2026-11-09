using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories;

public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await context.Employees.ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await context.Employees
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<Employee> AddAsync(Employee employee)
    {
        var entityEntry = await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public async Task UpdateAsync(Employee employee)
    {
        context.Employees.Update(employee);
        await context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employee is null) return;

        context.Employees.Remove(employee);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Employee>> SearchAsync(string? searchTerm)
    {
        var query = context.Employees.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(c => c.FullName.Contains(searchTerm) ||
                                     c.Email.Contains(searchTerm));
        }

        return await query.ToListAsync();
    }
}
