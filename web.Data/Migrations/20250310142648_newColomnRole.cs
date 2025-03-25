using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class newColomnRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserList",
                newName: "PasswordHash");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UserList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "UserList",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UserList");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "UserList");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "UserList",
                newName: "Password");
        }
    }
}
