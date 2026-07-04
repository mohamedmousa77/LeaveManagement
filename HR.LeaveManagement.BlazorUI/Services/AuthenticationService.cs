using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        public AuthenticationService(ILocalStorageService localStorageService, IClient client) 
            : base(localStorageService, client)
        {
        }

        public async Task<bool> AuthenticationAsync(string email, string password)
        {
            try
            {
                AuthRequest request = new AuthRequest
                {
                    Email = email,
                    Password = password
                };

                var authResponse = await _client.LoginAsync(request);
                if (authResponse.Token != string.Empty)
                {
                    await _localStorageService.SetItemAsync("token", authResponse.Token);
                    // Set Claims in Blazor and login state
                    return true;
                }
                return false;
            }
            catch (ApiException)
            {
                return false;
            }        
        }

        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("token");
            // Remove the claims in Blazor and Invalidate the login state
        }

        public async Task<bool> RegisterAsync(string email, string password, string firstName, string lastName, string userName)
        {
            RegistrationRequest request = new RegistrationRequest
            {
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                UserName = userName
            };

            var response = await _client.RegisterAsync(request);
            if (response.UserId != string.Empty)
            {
                return true;
            }
            return false;
        }
    }
}
