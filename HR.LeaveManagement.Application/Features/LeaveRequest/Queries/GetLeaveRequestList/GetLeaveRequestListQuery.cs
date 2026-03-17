using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestListDTO>>
{
}
