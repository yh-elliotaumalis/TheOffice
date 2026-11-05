using System;
using System.Collections.Generic;
using System.Text;
using TheOffice.Domain.Entities;

namespace TheOffice.Application.Interfaces.Repositories;

public interface IEmployeeAbsenceRepository
{
    Task<IEnumerable<EmployeeAbsence>> GetAllAsync();
    Task<EmployeeAbsence?> GetByIdAsync(int id);
    Task<EmployeeAbsence> AddAsync(EmployeeAbsence employeeAbsence);
    Task UpdateAsync(EmployeeAbsence employeeAbsence);
    Task DeleteAsync(int id);
}
