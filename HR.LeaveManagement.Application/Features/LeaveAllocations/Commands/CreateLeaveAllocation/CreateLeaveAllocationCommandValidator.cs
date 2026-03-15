using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocatio;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public CreateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
    {
        this._leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(l => l.LeaveTypeId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
            .MustAsync(LeaveTypeMustExist).WithMessage("Leave type does not exist");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveAllocationRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}
