using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveAllocationQueryHandler> _logger;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveAllocationCommandHandler(
        IMapper mapper,
        IAppLogger<GetLeaveAllocationQueryHandler> logger,
        ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository
        )
    {
        this._mapper = mapper;
        this._logger = logger;
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._leaveTypeRepository = leaveTypeRepository;
    }

    public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLeaveAllocationCommandValidator(_leaveAllocationRepository, _leaveTypeRepository);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())        
            throw new BadRequestException("Invalid Leave Allocation", validationResult);
        

        var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        //if(leaveAllocation is null)
        //{
        //    throw new NotFoundException(nameof(leaveAllocation), request.Id);
        //}

        _mapper.Map(request, leaveAllocation);

        await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

        return Unit.Value;
    }
}
