using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace web.Data.Migrations
{
    /// <inheritdoc />
    public partial class data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChallengeList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WinnerCreationId = table.Column<int>(type: "int", nullable: false),
                    CountCreations = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CreationList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountLike = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreationList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreationList_UserList_UserId",
                        column: x => x.UserId,
                        principalTable: "UserList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeCreation",
                columns: table => new
                {
                    ChallengeCreationListId = table.Column<int>(type: "int", nullable: false),
                    CreationChallengeListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeCreation", x => new { x.ChallengeCreationListId, x.CreationChallengeListId });
                    table.ForeignKey(
                        name: "FK_ChallengeCreation_ChallengeList_CreationChallengeListId",
                        column: x => x.CreationChallengeListId,
                        principalTable: "ChallengeList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeCreation_CreationList_ChallengeCreationListId",
                        column: x => x.ChallengeCreationListId,
                        principalTable: "CreationList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeCreation_CreationChallengeListId",
                table: "ChallengeCreation",
                column: "CreationChallengeListId");

            migrationBuilder.CreateIndex(
                name: "IX_CreationList_UserId",
                table: "CreationList",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeCreation");

            migrationBuilder.DropTable(
                name: "ChallengeList");

            migrationBuilder.DropTable(
                name: "CreationList");

            migrationBuilder.DropTable(
                name: "UserList");
        }
    }
}
