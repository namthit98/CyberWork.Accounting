using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberWork.Accounting.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnderOrganizationId",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnderOrganizationId",
                table: "Organizations");
        }
    }
}
