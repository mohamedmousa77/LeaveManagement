using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{

    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        this._leaveTypeRepository = leaveTypeRepository;

        RuleFor(blr => blr.LeaveTypeId)
            .GreaterThan(0).WithMessage("{PropertyName} is required")
            .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist.");

        RuleFor(blr => blr.StartDate)
                .LessThan(blr => blr.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

        RuleFor(blr => blr.EndDate)
                .GreaterThan(blr => blr.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");
    }

    private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);
        return leaveType != null;
    }
}
