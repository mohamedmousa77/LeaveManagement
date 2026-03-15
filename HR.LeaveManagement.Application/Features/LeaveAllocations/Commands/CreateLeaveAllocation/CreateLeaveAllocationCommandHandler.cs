using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocatio;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{

    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveAllocationQueryHandler> _logger;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public CreateLeaveAllocationCommandHandler(
        IMapper mapper,
        IAppLogger<GetLeaveAllocationQueryHandler> logger,
        ILeaveAllocationRepository leaveAllocationRepository
        )
    {
        this._mapper = mapper;
        this._logger = logger;
        this._leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        // Validator 
        var leaveAllocationValidator = new CreateLeaveAllocationCommandValidator(_leaveAllocationRepository);
        var validationResult = await leaveAllocationValidator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new BadRequestException("Invalid leave allocation request", validationResult);
        }

        var leaveType = await _leaveAllocationRepository.GetByIdAsync(request.LeaveTypeId);

        //  get Employee

        // get Period

        var Allocation = _mapper.Map<Domain.LeaveAllocation>(request);

        await _leaveAllocationRepository.CreateAsync(Allocation);

        return Unit.Value;


    }
}
