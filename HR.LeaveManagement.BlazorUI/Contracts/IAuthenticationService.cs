namespace HR.LeaveManagement.BlazorUI.Contracts;

public interface IAuthenticationService
{
    Task<bool> AuthenticationAsync(string email, string password);
    Task<bool> RegisterAsync
        (string email, string password, string firstName, string lastName, string userName);
    Task Logout();
}
