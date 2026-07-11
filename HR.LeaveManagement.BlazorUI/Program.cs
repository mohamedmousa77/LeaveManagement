using Blazored.LocalStorage;
using Blazored.Toast;
using HR.LeaveManagement.BlazorUI;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Handler;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Reflection;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<JwtAuthorizationMessageHandler>();
builder.Services.AddScoped<IClient>(sp =>
{
    var jwtHandler = sp.GetRequiredService<JwtAuthorizationMessageHandler>();
    // Ensure the delegating handler has an inner handler when creating HttpClient manually
    jwtHandler.InnerHandler = new HttpClientHandler();
    var http = new HttpClient(jwtHandler)
    {
        BaseAddress = new Uri("https://localhost:7241")
    };

    var client = new Client(http)
    {
        ReadResponseAsString = true
    };

    return client;
});

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<ApiAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<ApiAuthenticationProvider>());

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
builder.Services.AddScoped<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


builder.Services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
