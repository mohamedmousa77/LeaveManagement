using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails.LeaveTypeDetailsDTO;

public record GetLeaveTypeDetailsQuery(int id) : IRequest<LeaveTypeDetailsDTO>;

