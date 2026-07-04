using Blazored.LocalStorage;
using System.ComponentModel.Design;

namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        protected readonly ILocalStorageService _localStorageService;

        public BaseHttpService(ILocalStorageService localStorageService, IClient client)
        {
            this._localStorageService = localStorageService;
            _client = client;
        }
        protected Response<Guid> ConvertApiException<Guid>(ApiException exception)
        {
            if(exception.StatusCode == 404)
            {
                return new Response<Guid>()
                {
                    Message = "The record was not found.",
                    Success = false
                };
            }
            else if(exception.StatusCode == 400)
            {
                return new Response<Guid>()
                {
                    Message = "Invalid data was submitted.",
                    Success = false
                };                
            }
            else
            {
                return new Response<Guid>
                {
                    Message = "Something went wrong, please retry.",
                    Success = false
                };
            }
        }
    }
}
