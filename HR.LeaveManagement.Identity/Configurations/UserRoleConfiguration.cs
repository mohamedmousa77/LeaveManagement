using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "d0a516a8-4edf-447b-884a-b42a962abcc2", 
                UserId = "669d8085-141f-4f52-8d55-31d741cfc7c2"  
            },
            new IdentityUserRole<string>
            {
                RoleId = "8baadf52-9bbb-4aba-ad78-eb8d9d414e35",
                UserId = "0b9005dd-255c-44bd-94c7-72b189cad3dc",
            }
        );
    }
}
