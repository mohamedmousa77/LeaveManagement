
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Security.Claims;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        this._httpContextAccessor = httpContextAccessor;
    }

    public UserManager<ApplicationUser> UserManager { get; }

    public async Task<Employee> GetEmployee(string userId)
    {
        var employee = await _userManager.FindByIdAsync(userId);
        return new Employee
        {
            Email = employee.Email,
            FirstName = employee.FirstName,
            Id = employee.Id,
            LastName = employee.LastName,
        };

    }

    public async Task<List<Employee>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");

        return employees.Select(q => new Employee
        {
            Email = q.Email,
            FirstName = q.FirstName,
            Id = q.Id,
            LastName = q.LastName,
        }).ToList();
    }

    public string UserId {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return null!;

            // The token sets the user's id with the "uid" claim in AuthService.
            // Fallback to ClaimTypes.NameIdentifier if present for other token formats.
            return httpContext.User?.FindFirstValue("uid")
                ?? httpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
        }
    }
}
