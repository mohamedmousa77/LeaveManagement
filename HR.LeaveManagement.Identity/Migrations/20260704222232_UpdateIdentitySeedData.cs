using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentitySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b9005dd-255c-44bd-94c7-72b189cad3dc",
                column: "PasswordHash",
                value: "1@Password");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "669d8085-141f-4f52-8d55-31d741cfc7c2",
                column: "PasswordHash",
                value: "1@Password");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b9005dd-255c-44bd-94c7-72b189cad3dc",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAELng1V2tNLXJUyrFA0cUG9dBgq7dahyD3FBIdUNFnO6O2m1pU/882YXq+0LXnTtbpQ==");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "669d8085-141f-4f52-8d55-31d741cfc7c2",
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEBnLtzqpVGHTidyRiQ+ZGd3IcYTyJgOd5WAAwiLYOTNAfhr8D6TooXOp8Z0yysZZEQ==");
        }
    }
}
