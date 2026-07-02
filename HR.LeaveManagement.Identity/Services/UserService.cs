
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Models.Identity;
using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace HR.LeaveManagement.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
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
}
