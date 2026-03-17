using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequestToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequestToDelete == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        await _leaveRequestRepository.DeleteAsync(leaveRequestToDelete);

        return Unit.Value;
    }
}
