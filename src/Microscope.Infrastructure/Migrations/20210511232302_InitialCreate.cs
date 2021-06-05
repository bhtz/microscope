using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microscope.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mcsp_common");

            migrationBuilder.CreateTable(
                name: "Analytics",
                schema: "mcsp_common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Dimension = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analytics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RemoteConfigs",
                schema: "mcsp_common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Dimension = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteConfigs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analytics",
                schema: "mcsp_common");

            migrationBuilder.DropTable(
                name: "RemoteConfigs",
                schema: "mcsp_common");
        }
    }
}
