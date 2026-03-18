namespace HR.LeaveManagement.Application.Models.Email;

public class EmailSettings
{
    public string ApiUrl { get; set; }
    public string AuthToken { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
}
