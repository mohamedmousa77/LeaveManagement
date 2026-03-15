using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;

public class LeaveAllocationDTO
{
    public int Id { get; set; }
    public int NumberOfDays { get; set; }
    public LeaveTypeDTO LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set;  }
}
