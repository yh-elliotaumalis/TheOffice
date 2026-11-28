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
        IProjectRepository projectRepository,
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
                (user.IsInRole("Admin") || user.IsInRole("Employee")))
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
                (user.IsInRole("Admin") || user.IsInRole("Employee")))
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

        public async Task<decimal> GetReportedHoursAsync(int projectId)
        {
            await CheckAdminAsync();

            var project = await projectRepository.GetByIdAsync(projectId);

            if (project == null)
            {
                throw new ArgumentException("Projektet hittades inte.");
            }

            return await invoiceRepository.GetReportedHoursAsync(projectId);
        }

        public async Task<Invoice> AddAsync(Invoice invoice)
        {
            await CheckAdminAsync();

            if (invoice.CustomerId <= 0)
            {
                throw new ArgumentException("Välj en kund.");
            }

            if (invoice.ProjectId <= 0)
            {
                throw new ArgumentException("Välj ett projekt.");
            }

            if (invoice.HourlyRate <= 0 ||
                invoice.HourlyRate != Math.Round(invoice.HourlyRate, 2))
            {
                throw new ArgumentException(
                    "Ange ett timpris över 0 med högst två decimaler.");
            }

            if (invoice.DateSent == default ||
                invoice.DeadLine == default)
            {
                throw new ArgumentException(
                    "Ange fakturadatum och förfallodatum.");
            }

            if (invoice.DeadLine < invoice.DateSent)
            {
                throw new ArgumentException(
                    "Förfallodatum får inte vara före fakturadatum.");
            }

            var project = await projectRepository.GetByIdAsync(
                invoice.ProjectId);

            if (project == null ||
                project.CustomerId != invoice.CustomerId)
            {
                throw new ArgumentException(
                    "Projektet måste tillhöra den valda kunden.");
            }

            var hours = await invoiceRepository.GetReportedHoursAsync(
                invoice.ProjectId);

            if (hours <= 0)
            {
                throw new ArgumentException(
                    "Projektet har ingen registrerad arbetstid.");
            }

            invoice.TotalAmount = Math.Round(
                hours * invoice.HourlyRate,
                2,
                MidpointRounding.AwayFromZero);

            if (invoice.TotalAmount <= 0)
            {
                throw new ArgumentException(
                    "Fakturabeloppet måste vara över 0.");
            }

            invoice.InvoiceNumber =
                "INV-" + Guid.NewGuid().ToString("N").ToUpper();

            invoice.IsPaid = false;

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

        //public async Task<decimal> GetReportedHoursAsync(int projectId)
        //{
        //    await CheckAdminAsync();

        //    return await invoiceRepository.GetReportedHoursAsync(projectId);
        //}

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