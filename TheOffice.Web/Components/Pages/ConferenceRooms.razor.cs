using TheOffice.Application.Interfaces.Services;
using TheOffice.Domain.Entities;

namespace TheOffice.Web.Components.Pages;

public partial class ConferenceRooms
{
    private readonly IConferenceRoomService _conferenceRoomService;
    private IEnumerable<ConferenceRoom> conferenceRooms;
    private string search = "";
    
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
}
