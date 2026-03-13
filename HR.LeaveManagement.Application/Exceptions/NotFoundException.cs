namespace HR.LeaveManagement.Application.Exceptions;

public partial class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"{name} ({key}) was not found")
    {

    }
}
