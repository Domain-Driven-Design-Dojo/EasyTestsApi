using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class RenamePeopleToPersons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccUsers_CrePeople_FPeopleId",
                table: "AccUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CreCompanies_CrePeople_FPeopleId",
                table: "CreCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_CreIndividuals_CrePeople_FPeopleId",
                table: "CreIndividuals");

            migrationBuilder.DropForeignKey(
                name: "FK_CrePeople_CrePeopleTypes_FPeopleTypesId",
                table: "CrePeople");

            migrationBuilder.DropTable(
                name: "CrePeopleTypes");

            migrationBuilder.DropIndex(
                name: "IX_AccUsers_FPeopleId",
                table: "AccUsers");

            migrationBuilder.RenameColumn(
                name: "FPeopleId",
                table: "HrForgetPasswords",
                newName: "FPersonsId");

            migrationBuilder.RenameColumn(
                name: "FPeopleTypesId",
                table: "CrePeople",
                newName: "FPersonsTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_CrePeople_FPeopleTypesId",
                table: "CrePeople",
                newName: "IX_CrePeople_FPersonsTypesId");

            migrationBuilder.RenameColumn(
                name: "FPeopleId",
                table: "CreIndividuals",
                newName: "FPersonsId");

            migrationBuilder.RenameIndex(
                name: "IX_CreIndividuals_FPeopleId",
                table: "CreIndividuals",
                newName: "IX_CreIndividuals_FPersonsId");

            migrationBuilder.RenameColumn(
                name: "FPeopleId",
                table: "CreCompanies",
                newName: "FPersonsId");

            migrationBuilder.RenameIndex(
                name: "IX_CreCompanies_FPeopleId",
                table: "CreCompanies",
                newName: "IX_CreCompanies_FPersonsId");

            migrationBuilder.RenameColumn(
                name: "FPeopleId",
                table: "AccUsers",
                newName: "FPersonsId");

            migrationBuilder.CreateTable(
                name: "CrePersonTypes",
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
                    table.PrimaryKey("PK_CrePersonTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrePersonTypes_AccUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrePersonTypes_AccUsers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CrePersonTypes",
                columns: new[] { "Id", "CreationDate", "CreatorId", "IsActive", "LatinTitle", "ModificationDate", "ModifierId", "Title" },
                values: new object[] { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, true, "Individual", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "حقیقی" });

            migrationBuilder.InsertData(
                table: "CrePersonTypes",
                columns: new[] { "Id", "CreationDate", "CreatorId", "IsActive", "LatinTitle", "ModificationDate", "ModifierId", "Title" },
                values: new object[] { 2, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, true, "Company", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "حقوقی" });

            migrationBuilder.CreateIndex(
                name: "IX_AccUsers_FPersonsId",
                table: "AccUsers",
                column: "FPersonsId",
                unique: true,
                filter: "[FPersonsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CrePersonTypes_CreatorId",
                table: "CrePersonTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CrePersonTypes_ModifierId",
                table: "CrePersonTypes",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccUsers_CrePeople_FPersonsId",
                table: "AccUsers",
                column: "FPersonsId",
                principalTable: "CrePeople",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreCompanies_CrePeople_FPersonsId",
                table: "CreCompanies",
                column: "FPersonsId",
                principalTable: "CrePeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreIndividuals_CrePeople_FPersonsId",
                table: "CreIndividuals",
                column: "FPersonsId",
                principalTable: "CrePeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrePeople_CrePersonTypes_FPersonsTypesId",
                table: "CrePeople",
                column: "FPersonsTypesId",
                principalTable: "CrePersonTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccUsers_CrePeople_FPersonsId",
                table: "AccUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CreCompanies_CrePeople_FPersonsId",
                table: "CreCompanies");

            migrationBuilder.DropForeignKey(
                name: "FK_CreIndividuals_CrePeople_FPersonsId",
                table: "CreIndividuals");

            migrationBuilder.DropForeignKey(
                name: "FK_CrePeople_CrePersonTypes_FPersonsTypesId",
                table: "CrePeople");

            migrationBuilder.DropTable(
                name: "CrePersonTypes");

            migrationBuilder.DropIndex(
                name: "IX_AccUsers_FPersonsId",
                table: "AccUsers");

            migrationBuilder.RenameColumn(
                name: "FPersonsId",
                table: "HrForgetPasswords",
                newName: "FPeopleId");

            migrationBuilder.RenameColumn(
                name: "FPersonsTypesId",
                table: "CrePeople",
                newName: "FPeopleTypesId");

            migrationBuilder.RenameIndex(
                name: "IX_CrePeople_FPersonsTypesId",
                table: "CrePeople",
                newName: "IX_CrePeople_FPeopleTypesId");

            migrationBuilder.RenameColumn(
                name: "FPersonsId",
                table: "CreIndividuals",
                newName: "FPeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_CreIndividuals_FPersonsId",
                table: "CreIndividuals",
                newName: "IX_CreIndividuals_FPeopleId");

            migrationBuilder.RenameColumn(
                name: "FPersonsId",
                table: "CreCompanies",
                newName: "FPeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_CreCompanies_FPersonsId",
                table: "CreCompanies",
                newName: "IX_CreCompanies_FPeopleId");

            migrationBuilder.RenameColumn(
                name: "FPersonsId",
                table: "AccUsers",
                newName: "FPeopleId");

            migrationBuilder.CreateTable(
                name: "CrePeopleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifierId = table.Column<long>(type: "bigint", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LatinTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrePeopleTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrePeopleTypes_AccUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CrePeopleTypes_AccUsers_ModifierId",
                        column: x => x.ModifierId,
                        principalTable: "AccUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CrePeopleTypes",
                columns: new[] { "Id", "CreationDate", "CreatorId", "IsActive", "LatinTitle", "ModificationDate", "ModifierId", "Title" },
                values: new object[] { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, true, "Individual", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "حقیقی" });

            migrationBuilder.InsertData(
                table: "CrePeopleTypes",
                columns: new[] { "Id", "CreationDate", "CreatorId", "IsActive", "LatinTitle", "ModificationDate", "ModifierId", "Title" },
                values: new object[] { 2, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, true, "Company", new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "حقوقی" });

            migrationBuilder.CreateIndex(
                name: "IX_AccUsers_FPeopleId",
                table: "AccUsers",
                column: "FPeopleId",
                unique: true,
                filter: "[FPeopleId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CrePeopleTypes_CreatorId",
                table: "CrePeopleTypes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CrePeopleTypes_ModifierId",
                table: "CrePeopleTypes",
                column: "ModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccUsers_CrePeople_FPeopleId",
                table: "AccUsers",
                column: "FPeopleId",
                principalTable: "CrePeople",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CreCompanies_CrePeople_FPeopleId",
                table: "CreCompanies",
                column: "FPeopleId",
                principalTable: "CrePeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CreIndividuals_CrePeople_FPeopleId",
                table: "CreIndividuals",
                column: "FPeopleId",
                principalTable: "CrePeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CrePeople_CrePeopleTypes_FPeopleTypesId",
                table: "CrePeople",
                column: "FPeopleTypesId",
                principalTable: "CrePeopleTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
