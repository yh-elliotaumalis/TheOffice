using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Repositories;

public class CustomerRepository(ApplicationDbContext context) : ICustomerRepository
{
    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await context.Customers
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<Customer> AddAsync(Customer customer)
    {
        var entityEntry = await context.Customers.AddAsync(customer);
        await context.SaveChangesAsync();
        return entityEntry.Entity;
    }
    public async Task UpdateAsync(Customer customer)
    {
        context.Customers.Update(customer);
        await context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var customer = await context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        if (customer is null) return;

        context.Customers.Remove(customer);
        await context.SaveChangesAsync();
    }
}
