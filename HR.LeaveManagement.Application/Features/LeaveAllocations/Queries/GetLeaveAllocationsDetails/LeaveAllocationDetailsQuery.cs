using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationsDetails;

public class LeaveAllocationDetailsQuery : IRequest<LeaveAllocationDetailsDTO>
{
    public int Id { get; set; }
}

