using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HR.LeaveManagement.Identity.DbContext;

public class HrLeaveManagementDbContext : IdentityDbContext<ApplicationUser>
{
    public HrLeaveManagementDbContext(DbContextOptions<HrLeaveManagementDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(HrLeaveManagementDbContext).Assembly);
    }
}
