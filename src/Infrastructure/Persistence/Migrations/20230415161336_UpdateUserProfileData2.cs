using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberWork.Accounting.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserProfileData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Addres",
                table: "UserProfiles",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "UserProfiles",
                newName: "Addres");
        }
    }
}
