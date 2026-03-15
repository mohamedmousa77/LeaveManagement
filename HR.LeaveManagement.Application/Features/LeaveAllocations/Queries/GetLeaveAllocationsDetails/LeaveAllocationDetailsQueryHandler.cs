using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationsDetails;

public class LeaveAllocationDetailsQueryHandler : IRequestHandler<LeaveAllocationDetailsQuery, LeaveAllocationDetailsDTO>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveAllocationQueryHandler> _logger;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public LeaveAllocationDetailsQueryHandler(
        IMapper mapper,
        IAppLogger<GetLeaveAllocationQueryHandler> logger,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        this._mapper = mapper;
        this._logger = logger;
        this._leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task<LeaveAllocationDetailsDTO> Handle(LeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocationDetails = 
            await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

        return _mapper.Map<LeaveAllocationDetailsDTO>(leaveAllocationDetails);


    }
}
