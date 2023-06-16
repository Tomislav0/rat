using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureAndPictureGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Gallery");

            migrationBuilder.CreateTable(
                name: "PictureGroup",
                schema: "Gallery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                schema: "Gallery",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakenAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrictureGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Picture_PictureGroup_PrictureGroupId",
                        column: x => x.PrictureGroupId,
                        principalSchema: "Gallery",
                        principalTable: "PictureGroup",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Picture_PrictureGroupId",
                schema: "Gallery",
                table: "Picture",
                column: "PrictureGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Picture",
                schema: "Gallery");

            migrationBuilder.DropTable(
                name: "PictureGroup",
                schema: "Gallery");
        }
    }
}
