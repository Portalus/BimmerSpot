using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BimmerSpot.Migrations
{
    /// <inheritdoc />
    public partial class UserToSpotManyToManyRelationshipConfigured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Spots_SpotId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Spots_AspNetUsers_CreatedById",
                table: "Spots");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_SpotId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SpotId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUserSpot",
                columns: table => new
                {
                    AttendantsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttendedSpotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserSpot", x => new { x.AttendantsId, x.AttendedSpotsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserSpot_AspNetUsers_AttendantsId",
                        column: x => x.AttendantsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserSpot_Spots_AttendedSpotsId",
                        column: x => x.AttendedSpotsId,
                        principalTable: "Spots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserSpot_AttendedSpotsId",
                table: "ApplicationUserSpot",
                column: "AttendedSpotsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Spots_AspNetUsers_CreatedById",
                table: "Spots",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spots_AspNetUsers_CreatedById",
                table: "Spots");

            migrationBuilder.DropTable(
                name: "ApplicationUserSpot");

            migrationBuilder.AddColumn<int>(
                name: "SpotId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SpotId",
                table: "AspNetUsers",
                column: "SpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Spots_SpotId",
                table: "AspNetUsers",
                column: "SpotId",
                principalTable: "Spots",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Spots_AspNetUsers_CreatedById",
                table: "Spots",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
