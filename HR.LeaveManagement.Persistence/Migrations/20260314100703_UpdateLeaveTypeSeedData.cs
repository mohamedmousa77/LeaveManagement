using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLeaveTypeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2026, 3, 14, 10, 49, 28, 495, DateTimeKind.Local).AddTicks(3214), new DateTime(2026, 3, 14, 10, 49, 28, 491, DateTimeKind.Local).AddTicks(5760) });
        }
    }
}
