using TheOffice.Application.Interfaces.Repositories;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using TheOffice.Web.Data;
using Microsoft.AspNetCore.Components.Authorization;

namespace TheOffice.Web.Services
{
    public class TimeReportService(
        ITimeReportRepository timeReportRepository,
        UserManager<ApplicationUser> userManager,
        AuthenticationStateProvider authenticationStateProvider)
        : ITimeReportService
    {
        public Task<TimeReport> AddAsync(TimeReport timeReport)
        {
            return timeReportRepository.AddAsync(timeReport);
        }

        public Task DeleteAsync(int id)
        {
            return timeReportRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<TimeReport>> GetAllAsync()
        {
            return timeReportRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TimeReport>> GetByEmployeeAndPeriodAsync(
            int employeeId,
            DateOnly fromDate,
            DateOnly toDate)
        {
            var authenticationState =
                await authenticationStateProvider.GetAuthenticationStateAsync();

            var user = authenticationState.User;

            if (user.Identity?.IsAuthenticated != true)
            {
                throw new UnauthorizedAccessException(
                    "Du måste vara inloggad.");
            }

            if (employeeId <= 0)
            {
                throw new ArgumentException("Välj en anställd.");
            }

            if (fromDate > toDate)
            {
                throw new ArgumentException(
                    "Från-datum får inte vara efter till-datum.");
            }

            if (!user.IsInRole("Admin"))
            {
                var account = await userManager.GetUserAsync(user);

                if (account?.EmployeeId != employeeId)
                {
                    throw new UnauthorizedAccessException(
                        "Du kan bara se din egen arbetstid.");
                }
            }

            return await timeReportRepository.GetByEmployeeAndPeriodAsync(
                employeeId,
                fromDate,
                toDate);
        }

        public Task<TimeReport?> GetByIdAsync(int id)
        {
            return timeReportRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(TimeReport timeReport)
        {
            return timeReportRepository.UpdateAsync(timeReport);
        }
    }
}