using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace microscope.Migrations
{
    public partial class InitialCreateBaas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mcsp");

            migrationBuilder.CreateTable(
                name: "Analytic",
                schema: "mcsp",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Dimension = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analytic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RemoteConfig",
                schema: "mcsp",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemoteConfig", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analytic",
                schema: "mcsp");

            migrationBuilder.DropTable(
                name: "RemoteConfig",
                schema: "mcsp");
        }
    }
}
