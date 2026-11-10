using System.ComponentModel.DataAnnotations;
using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Components.Pages;

public class CreateConferenceRoomModel
{
    [Required, MinLength(3), MaxLength(255)]
    public string Name { get; set; } = "";
}

public partial class ConferenceRooms
{
    private readonly IConferenceRoomService _conferenceRoomService;
    private IEnumerable<ConferenceRoom> conferenceRooms;
    private string search = "";
    private bool isCreating = false;
    private string? errorMessage = null;
    private bool success = false;
    private CreateConferenceRoomModel newConferenceRoom = new CreateConferenceRoomModel();

    public ConferenceRooms(IConferenceRoomService conferenceRoomService)
    {
        _conferenceRoomService = conferenceRoomService;
    }
    protected override async Task OnInitializedAsync()
    {
        await SearchConferenceRooms();
        await base.OnInitializedAsync();
    }

    private async Task SearchConferenceRooms()
    {
        var searchText = search.Trim().ToLower();
        var rooms = await _conferenceRoomService.GetAllAsync();

        conferenceRooms = rooms.Where(r => r.Name.ToLower().Contains(searchText));
    }

    private async Task ToggleCreateConferenceRoom()
    {
        isCreating = !isCreating;
        errorMessage = null;
        success = false;
        newConferenceRoom = new CreateConferenceRoomModel();
    }

    private async Task CreateConferenceRoom()
    {
        var conferenceRoomName = newConferenceRoom.Name.Trim();
        var conferenceRoom = new ConferenceRoom
        {
            Name = conferenceRoomName
        };

        try
        {
            await _conferenceRoomService.AddAsync(conferenceRoom);
            success = true;
            errorMessage = null;
            await SearchConferenceRooms();
            isCreating = false;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error creating conference room: {ex.Message}";
            success = false;
        }
    }
}
