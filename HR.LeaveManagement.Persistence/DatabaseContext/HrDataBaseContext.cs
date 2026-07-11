using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;


public class HrDataBaseContext : DbContext
{
    private readonly IUserService _userService;

    public HrDataBaseContext(DbContextOptions<HrDataBaseContext> options, IUserService userService) 
        : base(options)
    {
        this._userService = userService;
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }


    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDataBaseContext).Assembly);



        base.OnModelCreating(modelBuilder);
        
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
             .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.ModifiedDate = DateTime.UtcNow;
            entry.Entity.ModifiedBy = _userService.UserId;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
                entry.Entity.CreatedBy = _userService.UserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
