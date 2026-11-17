using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Components.Pages;

public class EditUserModel
{
    public List<string> Roles { get; set; } = [];
    public int? EmployeeId { get; set; }
    public int? CustomerId { get; set; }
}

public partial class UserDetails
{
    [Parameter]
    public string Id { get; set; } = "";

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmployeeService _employeeService;
    private readonly ICustomerService _customerService;

    private ApplicationUser? user;
    private IList<string> userRoles = [];
    private List<Employee> employees = [];
    private List<Customer> customers = [];
    private bool isLoading = true;
    private bool isEditing = false;
    private string? errorMessage = null;
    private bool success = false;
    private EditUserModel editUser = new EditUserModel();

    public UserDetails(
        UserManager<ApplicationUser> userManager,
        IEmployeeService employeeService,
        ICustomerService customerService)
    {
        _userManager = userManager;
        _employeeService = employeeService;
        _customerService = customerService;
    }

    protected override async Task OnInitializedAsync()
    {
        await LoadUser();
        employees = (await _employeeService.GetAllAsync()).OrderBy(e => e.FullName).ToList();
        customers = (await _customerService.GetAllAsync()).OrderBy(c => c.Name).ToList();
        isLoading = false;
        await base.OnInitializedAsync();
    }

    private async Task LoadUser()
    {
        user = await _userManager.Users
            .Include(u => u.Employee)
            .Include(u => u.Customer)
            .FirstOrDefaultAsync(u => u.Id == Id);

        if (user is not null)
        {
            userRoles = await _userManager.GetRolesAsync(user);
        }
    }

    private void ToggleEditUser()
    {
        isEditing = !isEditing;
        errorMessage = null;
        success = false;
        if (user != null)
        {
            editUser = new EditUserModel
            {
                Roles = userRoles.ToList(),
                EmployeeId = user.EmployeeId,
                CustomerId = user.CustomerId
            };
        }
    }

    private void ToggleRole(string role, bool isChecked)
    {
        if (isChecked && !editUser.Roles.Contains(role))
        {
            editUser.Roles.Add(role);
        }
        else
        {
            editUser.Roles.Remove(role);
        }
    }

    private async Task UpdateUser()
    {
        if (user == null)
        {
            errorMessage = "User not found.";
            return;
        }

        try
        {
            foreach (var role in userRoles)
            {
                if (!editUser.Roles.Contains(role))
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
            }

            foreach (var role in editUser.Roles)
            {
                if (!userRoles.Contains(role))
                {
                    await _userManager.AddToRoleAsync(user, role);
                }
            }

            user.EmployeeId = editUser.EmployeeId;
            user.CustomerId = editUser.CustomerId;
            await _userManager.UpdateAsync(user);

            await LoadUser();
            isEditing = false;
            errorMessage = null;
            success = true;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error updating user: {ex.Message}";
            success = false;
        }
    }
}
