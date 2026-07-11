using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace HR.LeaveManagement.BlazorUI.Pages.LeaveRequests;

public partial class EmployeeIndex
{
    [Inject] ILeaveRequestService leaveRequestService { get; set; }
    [Inject] NavigationManager navigationManager { get; set; }
    [Inject] IJSRuntime Js {  get; set; }
    public EmployeeLeaveRequestViewVM Model { get; set; } = new EmployeeLeaveRequestViewVM();
    public string Message { get; set; } = string.Empty;

    protected async override Task OnInitializedAsync()
    {
        Model = await leaveRequestService.GetUserLeaveRequests();
    }

    async Task CancelRequestAsync (int id)
    {
        var confirm = await Js.InvokeAsync<bool>("confirm", "Are you sure you want to cancel this request?");
        if (confirm)
        {
            var cancelResult = await leaveRequestService.CancelLeaveRequest(id);
            if (cancelResult.Success)
            {
                StateHasChanged();
            }
            else
            {
                Message = "Something went wrong. Please try again later.";
            }
        }
       
    }
}