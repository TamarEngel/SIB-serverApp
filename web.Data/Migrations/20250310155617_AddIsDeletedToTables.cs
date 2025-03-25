using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeletedToTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CreationList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ChallengeList",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CreationList");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ChallengeList");
        }
    }
}
