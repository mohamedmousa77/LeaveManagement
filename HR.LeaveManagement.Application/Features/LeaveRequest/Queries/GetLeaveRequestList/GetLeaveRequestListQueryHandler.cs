using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System.Runtime.InteropServices;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;

public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDTO>>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveRequestListQueryHandler> _logger;

    public GetLeaveRequestListQueryHandler(ILeaveRequestRepository leaveRequestRepository,
        IUserService userService,
        IMapper mapper,
        IAppLogger<GetLeaveRequestListQueryHandler> logger)
    {
        _leaveRequestRepository = leaveRequestRepository;
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<List<LeaveRequestListDTO>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = new List<Domain.LeaveRequest>();
        var requests = new List<LeaveRequestListDTO>();

        // If user is logged in 
        if (request.IsLoggedInUser)
        {
            var userId = _userService.UserId;
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

            var employee = await _userService.GetEmployee(userId);
            requests = _mapper.Map<List<LeaveRequestListDTO>>(leaveRequests);

            foreach(var req in requests)
            {
                req.Employee = employee;
            }

            // Log the info
            _logger.LogInformation("Leave requests were retrieved successfully");
        }else
        {
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            requests = _mapper.Map<List<LeaveRequestListDTO>>(leaveRequests);

            foreach (var req in requests)
            {
                req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
            }
            // Log the info
            _logger.LogInformation("Leave requests were retrieved successfully");
        }

        return requests;
    }
}
