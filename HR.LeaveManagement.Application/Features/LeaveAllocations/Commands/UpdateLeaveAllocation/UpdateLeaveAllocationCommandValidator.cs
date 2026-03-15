using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository,
        ILeaveTypeRepository leaveTypeRepository

        )
    {
        this._leaveAllocationRepository = leaveAllocationRepository;
        this._leaveTypeRepository = leaveTypeRepository;
        RuleFor(l => l.NumberOfDays)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0");

        RuleFor(l => l.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be greater than or equal to the current year");

        RuleFor(l => l.LeaveTypeId)
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0")
            .MustAsync(LeaveTypeMustExist).WithMessage("Leave type does not exist");

        RuleFor(l => l.Id)
            .NotNull()
            .MustAsync(LeaveAllocationMustExist).WithMessage("Leave type does not exist")
            .WithMessage("{PropertyName} must be provided");

    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveAllocationRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}
