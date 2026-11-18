using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheOffice.Web.Data;

namespace TheOffice.Web.Components.Pages;

public partial class ManageUsers
{
    private readonly UserManager<ApplicationUser> _userManager;
    private List<ApplicationUser>? allUsers;
    private IEnumerable<ApplicationUser>? users;
    private Dictionary<string, string> userRoles = new();
    private string search = "";

    public ManageUsers(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task OnInitializedAsync()
    {
        allUsers = await _userManager.Users
            .OrderBy(u => u.Email)
            .ToListAsync();

        foreach (var user in allUsers)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userRoles[user.Id] = roles.Any() ? string.Join(", ", roles) : "None";
        }

        SearchUsers();
        await base.OnInitializedAsync();
    }

    private void SearchUsers()
    {
        if (allUsers is null)
        {
            return;
        }

        var searchText = search.Trim().ToLower();

        users = allUsers.Where(u => (u.Email ?? "").ToLower().Contains(searchText));
    }
}
