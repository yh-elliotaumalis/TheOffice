using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Services
{
    public class InvoiceService(
        IInvoiceRepository invoiceRepository,
        UserManager<ApplicationUser> userManager,
        AuthenticationStateProvider authenticationStateProvider)
        : IInvoiceService
    {
        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            var authenticationState =
                await authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authenticationState.User;

            if (user.Identity?.IsAuthenticated == true &&
                user.IsInRole("Admin"))
            {
                return await invoiceRepository.GetAllAsync();
            }

            var customerId = await GetCustomerIdAsync();

            if (customerId == null)
            {
                return new List<Invoice>();
            }

            return await invoiceRepository.GetByCustomerIdAsync(
                customerId.Value);
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            var authenticationState =
                await authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authenticationState.User;

            if (user.Identity?.IsAuthenticated == true &&
                user.IsInRole("Admin"))
            {
                return await invoiceRepository.GetByIdAsync(id);
            }

            var customerId = await GetCustomerIdAsync();

            if (customerId == null)
            {
                return null;
            }

            var invoice = await invoiceRepository.GetByIdAsync(id);

            if (invoice?.CustomerId != customerId.Value)
            {
                return null;
            }

            return invoice;
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await CheckAdminAsync();

            return await invoiceRepository.AddAsync(invoice);
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            await CheckAdminAsync();

            await invoiceRepository.UpdateAsync(invoice);
        }

        public async Task DeleteAsync(int id)
        {
            await CheckAdminAsync();

            await invoiceRepository.DeleteAsync(id);
        }

        private async Task CheckAdminAsync()
        {
            var authenticationState =
                await authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authenticationState.User;

            if (user.Identity?.IsAuthenticated != true ||
                !user.IsInRole("Admin"))
            {
                throw new UnauthorizedAccessException(
                    "Du måste vara administratör.");
            }
        }

        private async Task<int?> GetCustomerIdAsync()
        {
            var authenticationState =
                await authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authenticationState.User;

            if (user.Identity?.IsAuthenticated != true ||
                !user.IsInRole("Customer"))
            {
                throw new UnauthorizedAccessException(
                    "Du har inte tillgång till fakturorna.");
            }

            var account = await userManager.GetUserAsync(user);

            return account?.CustomerId;
        }
    }
}