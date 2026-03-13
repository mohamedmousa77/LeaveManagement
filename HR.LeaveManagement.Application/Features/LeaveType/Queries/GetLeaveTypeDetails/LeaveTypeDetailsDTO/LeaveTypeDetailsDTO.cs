namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails.LeaveTypeDetailsDTO;

public class LeaveTypeDetailsDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
    public DateTime? CreatedAt {  get; set; }
    public DateTime? ModifiedAt {get; set; }
}
