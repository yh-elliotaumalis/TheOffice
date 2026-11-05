using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Services
{
    public class InvoiceService(IInvoiceRepository invoiceRepository) : IInvoiceService
    {
        public Task<Invoice> AddAsync(Invoice invoice)
        {
            return invoiceRepository.AddAsync(invoice);
        }

        public Task DeleteAsync(int id)
        {
            return invoiceRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return invoiceRepository.GetAllAsync();
        }

        public Task<Invoice?> GetByIdAsync(int id)
        {
            return invoiceRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Invoice invoice)
        {
            return invoiceRepository.UpdateAsync(invoice);
        }
    }
}
