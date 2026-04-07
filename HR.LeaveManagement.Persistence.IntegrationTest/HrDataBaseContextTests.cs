using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTest
{
    public class HrDataBaseContextTests
    {
        private HrDataBaseContext _hrDatabaseContext;

        public HrDataBaseContextTests()
        {
            var dbOptions = new DbContextOptionsBuilder<HrDataBaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _hrDatabaseContext = new HrDataBaseContext(dbOptions);
        }
        [Fact]
        public async Task Save_SetDateCreatedValue()
        {
            // Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Test Vacation"
            };

            // Act - What is the action? 
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            // Assert - How the result shall be? 
            leaveType.CreatedDate.ShouldNotBeNull();
        }

        [Fact]
        public async Task Save_SetDateModifiedValueAsync()
        {
            // Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 10,
                Name = "Integration Test"
            };

            // Act - What is the action? 
            await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
            await _hrDatabaseContext.SaveChangesAsync();

            // Assert - How the result shall be? 
            leaveType.ModifiedDate.ShouldNotBeNull();
        }
    }
}
