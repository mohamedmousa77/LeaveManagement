using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;


public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandHandler(
        IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository)
    {
        this._mapper = mapper;
        this._leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var allocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if(allocation == null)
            throw new NotFoundException(nameof(allocation), request.Id);
        
        await _leaveAllocationRepository.DeleteAsync(allocation);

        

    }
}
