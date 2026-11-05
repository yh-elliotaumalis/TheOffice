using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class EmployeeService(IEmployeeRepository employeeRepository) : IEmployeeService
    {
        public Task<Employee> AddAsync(Employee employee)
        {
            return employeeRepository.AddAsync(employee);
        }

        public Task DeleteAsync(int id)
        {
            return employeeRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Employee>> GetAllAsync()
        {
            return employeeRepository.GetAllAsync();
        }

        public Task<Employee?> GetByIdAsync(int id)
        {
            return employeeRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Employee employee)
        {
            return employeeRepository.UpdateAsync(employee);
        }
    }
}
