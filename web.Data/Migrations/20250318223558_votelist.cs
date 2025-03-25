using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class votelist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VotesList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreationId = table.Column<int>(type: "int", nullable: false),
                    VotedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotesList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotesList_CreationList_CreationId",
                        column: x => x.CreationId,
                        principalTable: "CreationList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VotesList_UserList_UserId",
                        column: x => x.UserId,
                        principalTable: "UserList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

           

            migrationBuilder.CreateIndex(
                name: "IX_VotesList_CreationId",
                table: "VotesList",
                column: "CreationId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesList_UserId",
                table: "VotesList",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "VotesList");
        }
    }
}
