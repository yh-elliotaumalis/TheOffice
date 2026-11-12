using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Components.Pages;

public class EditConferenceRoomModel
{
    [Required, MinLength(3), MaxLength(255)]
    public string Name { get; set; } = "";
}

public class CreateBookingModel
{
    [Required(ErrorMessage = "Enter a topic."), MaxLength(255)]
    public string Description { get; set; } = "";

    [Required(ErrorMessage = "Pick a date.")]
    public DateOnly? Date { get; set; } = DateOnly.FromDateTime(DateTime.Today);

    [Required(ErrorMessage = "Pick a start time.")]
    public TimeOnly? StartTime { get; set; }

    [Required(ErrorMessage = "Pick an end time.")]
    public TimeOnly? EndTime { get; set; }

    [Required(ErrorMessage = "Pick an employee.")]
    public int? EmployeeId { get; set; }

    [Required(ErrorMessage = "Pick a customer.")]
    public int? CustomerId { get; set; }
}

public partial class ConferenceRoomDetails
{
    [Parameter]
    public int Id { get; set; }

    private readonly IConferenceRoomService _conferenceRoomService;
    private readonly IBookingService _bookingService;
    private readonly IEmployeeService _employeeService;
    private readonly ICustomerService _customerService;
    private readonly NavigationManager _navigationManager;

    private bool isBooking = false;
    private bool isSavingBooking = false;
    private bool bookingSuccess = false;
    private string? bookingError = null;
    private CreateBookingModel newBooking = new CreateBookingModel();
    private List<Employee> employees = [];
    private List<Customer> customers = [];
    private ConferenceRoom? conferenceRoom;
    private bool isLoading = true;
    private bool isEditing = false;
    private string? errorMessage = null;
    private bool success = false;
    private bool showDeleteModal = false;
    private EditConferenceRoomModel editConferenceRoom = new EditConferenceRoomModel();

    public ConferenceRoomDetails(
        IConferenceRoomService conferenceRoomService,
        IBookingService bookingService,
        IEmployeeService employeeService,
        ICustomerService customerService,
        NavigationManager navigationManager)
    {
        _conferenceRoomService = conferenceRoomService;
        _bookingService = bookingService;
        _employeeService = employeeService;
        _customerService = customerService;
        _navigationManager = navigationManager;
    }

    protected override async Task OnInitializedAsync()
    {
        conferenceRoom = await _conferenceRoomService.GetByIdAsync(Id);
        employees = (await _employeeService.GetAllAsync()).OrderBy(e => e.FullName).ToList();
        customers = (await _customerService.GetAllAsync()).OrderBy(c => c.Name).ToList();
        isLoading = false;
        await base.OnInitializedAsync();
    }

    private void ToggleBookMeeting()
    {
        isBooking = !isBooking;
        bookingError = null;
        bookingSuccess = false;
        newBooking = new CreateBookingModel();
    }

    private async Task CreateBooking()
    {
        if (conferenceRoom == null || isSavingBooking)
        {
            return;
        }

        isSavingBooking = true;
        bookingError = null;
        bookingSuccess = false;

        try
        {
            var start = newBooking.Date!.Value.ToDateTime(newBooking.StartTime!.Value);
            var end = newBooking.Date!.Value.ToDateTime(newBooking.EndTime!.Value);

            if (end <= start)
            {
                bookingError = "End time must be after start time.";
                return;
            }

            if (await _bookingService.HasOverlapAsync(conferenceRoom.Id, start, end))
            {
                bookingError = "The room is already booked at that time.";
                return;
            }

            var booking = new TheOffice.Domain.Entities.Booking
            {
                Description = newBooking.Description.Trim(),
                StartTime = start,
                EndTime = end,
                EmployeeId = newBooking.EmployeeId.Value,
                CustomerId = newBooking.CustomerId.Value,
                ConferenceRoomId = conferenceRoom.Id
            };

            await _bookingService.AddAsync(booking);

            conferenceRoom = await _conferenceRoomService.GetByIdAsync(Id);
            isBooking = false;
            bookingSuccess = true;
            newBooking = new CreateBookingModel();
        }
        catch (Exception ex)
        {
            bookingError = $"Error creating booking: {ex.Message}";
        }
        finally
        {
            isSavingBooking = false;
        }
    }

    private async Task ToggleEditConferenceRoom()
    {
        isEditing = !isEditing;
        errorMessage = null;
        success = false;
        if (conferenceRoom != null)
        {
            editConferenceRoom.Name = conferenceRoom.Name;
        }
    }

    private async Task UpdateConferenceRoom()
    {
        if (conferenceRoom == null)
        {
            errorMessage = "Conference room not found.";
            return;
        }
        var conferenceRoomName = editConferenceRoom.Name.Trim();
        conferenceRoom.Name = conferenceRoomName;
        try
        {
            await _conferenceRoomService.UpdateAsync(conferenceRoom);
            isEditing = false;
            errorMessage = null;
            success = true;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error updating conference room: {ex.Message}";
            isEditing = true;
            success = false;
        }
    }

    private async Task OpenDeleteConfirmation()
    {
        showDeleteModal = true;
    }

    private async Task CloseDeleteConfirmation()
    {
        showDeleteModal = false;
    }

    private async Task DeleteConferenceRoom()
    {
        if (conferenceRoom == null)
        {
            errorMessage = "Conference room not found.";
            return;
        }
        try
        {
            await _conferenceRoomService.DeleteAsync(conferenceRoom.Id);
            _navigationManager.NavigateTo("/conference-rooms");
        }
        catch (Exception ex)
        {
            errorMessage = $"Error deleting conference room: {ex.Message}";
            success = false;
            showDeleteModal = false;
        }
    }
}
