using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly ApiAuthenticationProvider _apiAuthenticationProvider;

        public AuthenticationService(ILocalStorageService localStorageService, IClient client,
            ApiAuthenticationProvider apiAuthenticationProvider) : base(localStorageService, client)
        {
            _apiAuthenticationProvider = apiAuthenticationProvider;
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
                    await ((ApiAuthenticationProvider)_apiAuthenticationProvider).LoggedIn();               

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

            // Remove the claims in Blazor and Invalidate the login state
            await ((ApiAuthenticationProvider)_apiAuthenticationProvider).LoggedOut();
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
