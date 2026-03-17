using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailDTO>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestDetailQueryHandler> _logger;

    public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IAppLogger<GetLeaveRequestDetailQueryHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LeaveRequestDetailDTO> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

        var requestDto = _mapper.Map<LeaveRequestDetailDTO>(leaveRequest);

        _logger.LogInformation("Leave request detail retrieved successfully");

        return requestDto;
    }
}
