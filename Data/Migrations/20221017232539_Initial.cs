using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LatinTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrePeopleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LatinTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrePeopleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysErrorLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FUsersId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FSysErrorTypesId = table.Column<int>(type: "int", nullable: false),
                    InputJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatchedException = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysErrorLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysErrorTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysErrorTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysLogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FUsersId = table.Column<long>(type: "bigint", nullable: false),
                    FSystemOperationsId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: true),
                    LogDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccGroupRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FRolesId = table.Column<long>(type: "bigint", nullable: false),
                    FGroupsId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccGroupRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccGroupRoles_AccGroups_FGroupsId",
                        column: x => x.FGroupsId,
                        principalTable: "AccGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccGroupRoles_AccRoles_FRolesId",
                        column: x => x.FRolesId,
                        principalTable: "AccRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccRoleClaims_AccRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CrePeople",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FPeopleTypesId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrePeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrePeople_CrePeopleTypes_FPeopleTypesId",
                        column: x => x.FPeopleTypesId,
                        principalTable: "CrePeopleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmationCode = table.Column<int>(type: "int", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FPeopleId = table.Column<long>(type: "bigint", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccUsers_CrePeople_FPeopleId",
                        column: x => x.FPeopleId,
                        principalTable: "CrePeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CreCompanies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FPeopleId = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EconomicNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelNumbers = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreCompanies_CrePeople_FPeopleId",
                        column: x => x.FPeopleId,
                        principalTable: "CrePeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CreIndividuals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FPeopleId = table.Column<long>(type: "bigint", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreIndividuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreIndividuals_CrePeople_FPeopleId",
                        column: x => x.FPeopleId,
                        principalTable: "CrePeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HrForgetPasswords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FPeopleId = table.Column<long>(type: "bigint", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HrForgetPasswords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HrForgetPasswords_CrePeople_PersonId",
                        column: x => x.PersonId,
                        principalTable: "CrePeople",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccGroupUsers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    FGroupsId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccGroupUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccGroupUsers_AccGroups_FGroupsId",
                        column: x => x.FGroupsId,
                        principalTable: "AccGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccGroupUsers_AccUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccUserClaims_AccUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccUserRoles",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    RoleId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AccUserRoles_AccRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AccRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccUserRoles_AccUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccUsersLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccUsersLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AccUsersLogins_AccUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccUserTokens",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AccUserTokens_AccUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AccGroups",
                columns: new[] { "Id", "CreationDate", "CreatorId", "Description", "IsActive", "LatinTitle", "ModificationDate", "ModifierId", "Title" },
                values: new object[] { 1, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, null, true, "Super Admins", new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0L, "سوپرادمین ها" });

            migrationBuilder.InsertData(
                table: "AccRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName", "PersianName" },
                values: new object[] { 1L, "00000000-0000-0000-0000-000000000000", "Super admin's group", "SuperAdmin", "SUPERADMIN", "سوپرادمین" });

            migrationBuilder.InsertData(
                table: "AccUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "ConfirmationCode", "CreationDate", "Email", "EmailConfirmed", "FPeopleId", "IsActive", "LastLoginDate", "LockoutEnabled", "LockoutEnd", "ModificationDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "0ae777a1-1159-4a21-b4bb-fd2998062e6a", null, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "superadmin@contoso.com", true, null, true, null, false, null, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "SUPERADMIN@CONTOSO.COM", "SUPERADMIN", "AQAAAAEAACcQAAAAEDfEmt5lTB3ZSu0s2oBovId9paD120eNpego6TNzOjrOrIfS4QAs6TCnJb04wsjPsw==", null, true, "55f82b99-af47-426b-b09e-af2d57e39f77", false, "superadmin" });

            migrationBuilder.InsertData(
                table: "CrePeopleTypes",
                columns: new[] { "Id", "CreationDate", "CreatorId", "IsActive", "LatinTitle", "ModificationDate", "ModifierId", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, true, "Individual", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "حقیقی" },
                    { 2, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, true, "Company", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "حقوقی" }
                });

            migrationBuilder.InsertData(
                table: "AccGroupRoles",
                columns: new[] { "Id", "CreationDate", "CreatorId", "FGroupsId", "FRolesId", "IsActive", "ModificationDate", "ModifierId" },
                values: new object[] { 1L, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1, 1L, true, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L });

            migrationBuilder.InsertData(
                table: "AccGroupUsers",
                columns: new[] { "Id", "CreationDate", "CreatorId", "FGroupsId", "IsActive", "ModificationDate", "ModifierId", "UserId" },
                values: new object[] { 1L, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1, true, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1L });

            migrationBuilder.InsertData(
                table: "CrePeople",
                columns: new[] { "Id", "CreationDate", "CreatorId", "FPeopleTypesId", "IsActive", "ModificationDate", "ModifierId" },
                values: new object[] { 1L, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1, true, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L });

            migrationBuilder.InsertData(
                table: "CreIndividuals",
                columns: new[] { "Id", "FPeopleId", "FirstName", "LastName" },
                values: new object[] { 1L, 1L, "Super Admin", null });

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupRoles_FGroupsId",
                table: "AccGroupRoles",
                column: "FGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupRoles_FRolesId_FGroupsId",
                table: "AccGroupRoles",
                columns: new[] { "FRolesId", "FGroupsId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupUsers_FGroupsId_UserId",
                table: "AccGroupUsers",
                columns: new[] { "FGroupsId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccGroupUsers_UserId",
                table: "AccGroupUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccRoleClaims_RoleId",
                table: "AccRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AccRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccUserClaims_UserId",
                table: "AccUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AccUserRoles_RoleId",
                table: "AccUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AccUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AccUsers_FPeopleId",
                table: "AccUsers",
                column: "FPeopleId",
                unique: true,
                filter: "[FPeopleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AccUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AccUsersLogins_UserId",
                table: "AccUsersLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreCompanies_FPeopleId",
                table: "CreCompanies",
                column: "FPeopleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreCompanies_NationalId",
                table: "CreCompanies",
                column: "NationalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreIndividuals_FPeopleId",
                table: "CreIndividuals",
                column: "FPeopleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrePeople_FPeopleTypesId",
                table: "CrePeople",
                column: "FPeopleTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_HrForgetPasswords_PersonId",
                table: "HrForgetPasswords",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccGroupRoles");

            migrationBuilder.DropTable(
                name: "AccGroupUsers");

            migrationBuilder.DropTable(
                name: "AccRoleClaims");

            migrationBuilder.DropTable(
                name: "AccUserClaims");

            migrationBuilder.DropTable(
                name: "AccUserRoles");

            migrationBuilder.DropTable(
                name: "AccUsersLogins");

            migrationBuilder.DropTable(
                name: "AccUserTokens");

            migrationBuilder.DropTable(
                name: "CreCompanies");

            migrationBuilder.DropTable(
                name: "CreIndividuals");

            migrationBuilder.DropTable(
                name: "HrForgetPasswords");

            migrationBuilder.DropTable(
                name: "SysErrorLogs");

            migrationBuilder.DropTable(
                name: "SysErrorTypes");

            migrationBuilder.DropTable(
                name: "SysLogs");

            migrationBuilder.DropTable(
                name: "AccGroups");

            migrationBuilder.DropTable(
                name: "AccRoles");

            migrationBuilder.DropTable(
                name: "AccUsers");

            migrationBuilder.DropTable(
                name: "CrePeople");

            migrationBuilder.DropTable(
                name: "CrePeopleTypes");
        }
    }
}
