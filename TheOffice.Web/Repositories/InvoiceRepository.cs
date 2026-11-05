using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories
{
    public class InvoiceRepository(ApplicationDbContext context) : IInvoiceRepository
    {
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await context.Invoices.ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await context.Invoices
                .FirstOrDefaultAsync(i => i.Id == id);
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
            var invoice = await context.Invoices.FirstOrDefaultAsync(i => i.Id == id);
            if (invoice is null) return;

            context.Invoices.Remove(invoice);
            await context.SaveChangesAsync();
        }
    }
}
