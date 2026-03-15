using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;

public class GetLeaveAllocationQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDTO>>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveAllocationQueryHandler> _logger;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public GetLeaveAllocationQueryHandler(
        IMapper mapper, 
        IAppLogger<GetLeaveAllocationQueryHandler> logger, 
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        this._mapper = mapper;
        this._logger = logger;
        this._leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task<List<LeaveAllocationDTO>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
        // Remimber to implement: 
        // - Get Allocations for the specific user
        // - Get Allocations per Epmloyee

        var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();

        var data = _mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations);



        return data;
    }
}
