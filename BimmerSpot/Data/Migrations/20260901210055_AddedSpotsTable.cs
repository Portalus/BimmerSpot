using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BimmerSpot.Migrations
{
    /// <inheritdoc />
    public partial class AddedSpotsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpotId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Spots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spots_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SpotId",
                table: "AspNetUsers",
                column: "SpotId");

            migrationBuilder.CreateIndex(
                name: "IX_Spots_CreatedById",
                table: "Spots",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Spots_SpotId",
                table: "AspNetUsers",
                column: "SpotId",
                principalTable: "Spots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Spots_SpotId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Spots");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SpotId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpotId",
                table: "AspNetUsers");
        }
    }
}
