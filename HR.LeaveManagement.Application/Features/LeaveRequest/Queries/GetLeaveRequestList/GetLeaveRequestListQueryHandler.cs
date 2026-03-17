using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDTO>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestListQueryHandler> _logger;

    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository,
        IMapper mapper,
        IAppLogger<GetLeaveRequestListQueryHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LeaveRequestListDTO>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        // Query to DB
        // Check if we need to filter by user or get all (business requirement might dictate this later)
        // For now, getting all to match the LeaveType pattern
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();

        // Mapper
        var requests = _mapper.Map<List<LeaveRequestListDTO>>(leaveRequests);

        // Log the info
        _logger.LogInformation("Leave requests were retrieved successfully");

        return requests;
    }
}
