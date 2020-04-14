using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace com.ironhasura.Migrations
{
    public partial class IronHasuraIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IronHasuraRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronHasuraRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IronHasuraUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronHasuraUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IronHasuraRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronHasuraRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IronHasuraRoleClaims_IronHasuraRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IronHasuraRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IronHasuraUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronHasuraUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IronHasuraUserClaims_IronHasuraUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IronHasuraUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IronHasuraUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronHasuraUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_IronHasuraUserLogins_IronHasuraUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IronHasuraUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IronHasuraUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronHasuraUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_IronHasuraUserRoles_IronHasuraRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IronHasuraRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IronHasuraUserRoles_IronHasuraUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IronHasuraUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IronHasuraUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IronHasuraUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_IronHasuraUserTokens_IronHasuraUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "IronHasuraUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IronHasuraRoleClaims_RoleId",
                table: "IronHasuraRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "IronHasuraRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IronHasuraUserClaims_UserId",
                table: "IronHasuraUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IronHasuraUserLogins_UserId",
                table: "IronHasuraUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IronHasuraUserRoles_RoleId",
                table: "IronHasuraUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "IronHasuraUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "IronHasuraUsers",
                column: "NormalizedUserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IronHasuraRoleClaims");

            migrationBuilder.DropTable(
                name: "IronHasuraUserClaims");

            migrationBuilder.DropTable(
                name: "IronHasuraUserLogins");

            migrationBuilder.DropTable(
                name: "IronHasuraUserRoles");

            migrationBuilder.DropTable(
                name: "IronHasuraUserTokens");

            migrationBuilder.DropTable(
                name: "IronHasuraRoles");

            migrationBuilder.DropTable(
                name: "IronHasuraUsers");
        }
    }
}
