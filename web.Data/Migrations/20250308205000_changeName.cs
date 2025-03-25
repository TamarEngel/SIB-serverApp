using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class changeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CountLike",
                table: "CreationList",
                newName: "Votes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Votes",
                table: "CreationList",
                newName: "CountLike");
        }
    }
}
