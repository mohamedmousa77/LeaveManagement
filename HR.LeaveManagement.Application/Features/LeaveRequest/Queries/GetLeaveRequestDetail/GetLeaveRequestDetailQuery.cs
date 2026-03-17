using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQuery : IRequest<LeaveRequestDetailDTO>
{
    public int Id { get; set; }
}
