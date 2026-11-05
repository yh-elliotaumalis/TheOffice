using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public Task<Customer> AddAsync(Customer customer)
        {
            return customerRepository.AddAsync(customer);
        }

        public Task DeleteAsync(int id)
        {
            return customerRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return customerRepository.GetAllAsync();
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            return customerRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Customer customer)
        {
            return customerRepository.UpdateAsync(customer);
        }
    }
}
