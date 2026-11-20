using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;
using TheOffice.Web.Data;

namespace TheOffice.Web.Components.Pages;

public partial class Bookings
{
    [CascadingParameter]
    private Task<AuthenticationState> authenticationState { get; set; } = default!;
    private readonly IBookingService _bookingService;
    private readonly IConferenceRoomService _conferenceRoomService;
    private readonly ICustomerService _customerService;
    private readonly UserManager<ApplicationUser> _userManager;
    private List<Booking>? MyBookings;

    private int? currentEmployeeId;
    private int? currentCustomerId;
    public Bookings(IBookingService bookingService, IConferenceRoomService conferenceRoomService, ICustomerService customerService, UserManager<ApplicationUser> userManager)
    {
        _bookingService = bookingService;
        _conferenceRoomService = conferenceRoomService;
        _customerService = customerService;
        _userManager = userManager;
    }

    protected override async Task OnInitializedAsync()
    {
        var user = await _userManager.GetUserAsync((await authenticationState).User);
        currentEmployeeId = user?.EmployeeId;
        currentCustomerId = user?.CustomerId;

        if (currentEmployeeId is not null)
        {
            MyBookings = (await _bookingService.GetByEmployeeIdAsync(currentEmployeeId.Value)).ToList();
        }
        else if (currentCustomerId is not null)
        {
            MyBookings = (await _bookingService.GetByCustomerIdAsync(currentCustomerId.Value)).ToList();
        } else
        {
            MyBookings = new List<Booking>();
        }

        MyBookings = MyBookings.Where(b => b.StartTime >= DateTime.Now || (b.StartTime <= DateTime.Now && b.EndTime >= DateTime.Now)).ToList();

        await base.OnInitializedAsync();
    }
}
