using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.LeaveManagement.Identity.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "d0a516a8-4edf-447b-884a-b42a962abcc2",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "64e7676e-a13d-47cc-97c1-d3b53e6a5fa1"
            },
            new IdentityRole
            {
                Id = "8baadf52-9bbb-4aba-ad78-eb8d9d414e35",
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
                ConcurrencyStamp = "3645a2b6-9948-4031-8708-5de77d8b98ed"
            }
        );
    }

}
