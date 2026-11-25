using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class InvoiceRepository(ApplicationDbContext context)
        : IInvoiceRepository
    {
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await context.Invoices
                .Include(invoice => invoice.Customer)
                .ToListAsync();
        }

        public async Task<IEnumerable<Invoice>> GetByCustomerIdAsync(
            int customerId)
        {
            return await context.Invoices
                .Include(invoice => invoice.Customer)
                .Where(invoice => invoice.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await context.Invoices
                .Include(invoice => invoice.Customer)
                .FirstOrDefaultAsync(invoice => invoice.Id == id);
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            var entityEntry = await context.Invoices.AddAsync(invoice);

            await context.SaveChangesAsync();

            return entityEntry.Entity;
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            context.Invoices.Update(invoice);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await context.Invoices
                .FirstOrDefaultAsync(item => item.Id == id);

            if (invoice == null)
            {
                return;
            }

            context.Invoices.Remove(invoice);

            await context.SaveChangesAsync();
        }
    }
}