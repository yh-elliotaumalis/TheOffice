using System;
using System.Collections.Generic;
using System.Text;
using TheOffice.Domain.Entities;

namespace TheOffice.Application.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<Employee> AddAsync(Employee employee);
    Task UpdateAsync(Employee employee);
    Task DeleteAsync(int id);
    Task<IEnumerable<Employee>> SearchAsync(string? searchTerm);
}
