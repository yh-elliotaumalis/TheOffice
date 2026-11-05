using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class EmployeeAbsenceService(IEmployeeAbsenceRepository employeeAbsenceRepository) : IEmployeeAbsenceService
    {
        public Task<EmployeeAbsence> AddAsync(EmployeeAbsence employeeAbsence)
        {
            return employeeAbsenceRepository.AddAsync(employeeAbsence);
        }

        public Task DeleteAsync(int id)
        {
            return employeeAbsenceRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<EmployeeAbsence>> GetAllAsync()
        {
            return employeeAbsenceRepository.GetAllAsync();
        }

        public Task<EmployeeAbsence?> GetByIdAsync(int id)
        {
            return employeeAbsenceRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(EmployeeAbsence employeeAbsence)
        {
            return employeeAbsenceRepository.UpdateAsync(employeeAbsence);
        }
    }
}
