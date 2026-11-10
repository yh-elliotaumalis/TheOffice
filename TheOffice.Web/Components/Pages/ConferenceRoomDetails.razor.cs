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

public partial class ConferenceRoomDetails
{
    [Parameter]
    public int Id { get; set; }

    private readonly IConferenceRoomService _conferenceRoomService;
    private readonly NavigationManager _navigationManager;
    private ConferenceRoom? conferenceRoom;
    private bool isLoading = true;
    private bool isEditing = false;
    private string? errorMessage = null;
    private bool success = false;
    private bool showDeleteModal = false;
    private EditConferenceRoomModel editConferenceRoom = new EditConferenceRoomModel();

    public ConferenceRoomDetails(IConferenceRoomService conferenceRoomService, NavigationManager navigationManager)
    {
        _conferenceRoomService = conferenceRoomService;
        _navigationManager = navigationManager;
    }

    protected override async Task OnInitializedAsync()
    {
        conferenceRoom = await _conferenceRoomService.GetByIdAsync(Id);
        isLoading = false;
        await base.OnInitializedAsync();
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
