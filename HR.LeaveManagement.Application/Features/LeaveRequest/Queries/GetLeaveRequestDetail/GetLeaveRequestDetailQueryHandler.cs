using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailDTO>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestDetailQueryHandler> _logger;

    public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository,
        IUserService userService,
        IMapper mapper,
        IAppLogger<GetLeaveRequestDetailQueryHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<LeaveRequestDetailDTO> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

        var requestDto = _mapper.Map<LeaveRequestDetailDTO>(leaveRequest);

        // Populate the Employee details using the IUserService so the UI has the employee name
        if (!string.IsNullOrEmpty(requestDto.RequestingEmployeeId))
        {
            requestDto.Employee = await _userService.GetEmployee(requestDto.RequestingEmployeeId);
        }

        _logger.LogInformation("Leave request detail retrieved successfully");

        return requestDto;
    }
}
