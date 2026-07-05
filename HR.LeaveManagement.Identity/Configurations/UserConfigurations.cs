using HR.LeaveManagement.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        // Use fixed password hashes to keep deterministic model for EF Core migrations.
        builder.HasData(
            new ApplicationUser
            {
                Id = "669d8085-141f-4f52-8d55-31d741cfc7c2",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = "1@Password",
                ConcurrencyStamp = "a8250fc0-f888-44dd-961d-f12006fc569d",
                SecurityStamp = "d533e2e3-8cd8-41e1-aa7c-b46a792ab6b2",
                EmailConfirmed = true,
            },
            new ApplicationUser
            {
                Id = "0b9005dd-255c-44bd-94c7-72b189cad3dc",
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                PasswordHash = "1@Password",
                ConcurrencyStamp = "f2bdc758-cc20-4533-a9b0-8af7b99786c0",
                SecurityStamp = "6a63d974-3d39-4429-9d3c-36bf4a3a001e",
                EmailConfirmed = true,
            }
        );
    }
}
