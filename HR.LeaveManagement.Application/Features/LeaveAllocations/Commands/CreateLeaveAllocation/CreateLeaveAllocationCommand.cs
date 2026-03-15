using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocatio;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
    public int LeaveTypeId { get; set; }
}
